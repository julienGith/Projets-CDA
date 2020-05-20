using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPS.BOL;
using OPS.DAL;
using Microsoft.EntityFrameworkCore;
using OPS2020.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace OPS2020.Controllers
{
    public class OPSController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OPSCtx _context;

        public OPSController(OPSCtx context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Etape0(Etape0Model etape0)
        {
            return View("Etape0", etape0);
        }
        [HttpPost]
        public async Task<IActionResult> Etape0(Etape0Model etape0, string BtnValider)
        {
            return RedirectToAction("Quest", "OPS");
        }
        // Passage de l'étape 0 à l'étape quest en mode création, passage du code formation entre les vues via le cookie de session 
        [HttpGet]
        public async Task<IActionResult> Quest(Etape0Model etape0, QuestionnaireModel questionnaireModel)
        {
            if (HttpContext.Session.Get<Etape0Model>("etape0")!=null)
            {
                etape0 = HttpContext.Session.Get<Etape0Model>("etape0");
                questionnaireModel.codeProduitFormation = etape0.CodeProduitFormation;
            }
            return View("Quest", questionnaireModel);
        }
        // Sauvergarde du questionnaire dans la base de donnée ou mise à jour des données
        [HttpPost]
        public void CreerQuestionnaire(string data)
        {
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaireModel = JsonConvert.DeserializeObject<QuestionnaireModel>(data);
            if (questionnaireModel.questionnaireId != null)
            {
                questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == questionnaireModel.questionnaireId);
                questionnaire.CodeProduitFormation = questionnaireModel.codeProduitFormation;
                questionnaire.DataJson = JsonConvert.SerializeObject(questionnaireModel.listQuestObj);
                questionnaire.CodeHtml = questionnaireModel.codeHtml;
                questionnaire.Description = questionnaireModel.description;
                questionnaire.EtatQuestionnaire = questionnaireModel.etatQuestionnaire;
                questionnaire.TitreQuestionnaire = questionnaireModel.titreQuestionnaire;
            }
            else
            {
                questionnaire.CodeProduitFormation = questionnaireModel.codeProduitFormation;
                questionnaire.DataJson = JsonConvert.SerializeObject(questionnaireModel.listQuestObj);
                questionnaire.CodeHtml = questionnaireModel.codeHtml;
                questionnaire.Description = questionnaireModel.description;
                questionnaire.EtatQuestionnaire = questionnaireModel.etatQuestionnaire;
                questionnaire.TitreQuestionnaire = questionnaireModel.titreQuestionnaire;
                _context.Questionnaire.Add(questionnaire);
            }

            _context.SaveChanges();
        }
        // enregistrement chargement des questions dans un cookie non implémenté
        [HttpPost]
        public async Task<IActionResult> GetQuestionObj(string codeQuestion)
        {
            QuestionnaireModel questionnaire = new QuestionnaireModel();
            QuestionOBJ questionOBJ = new QuestionOBJ();
            questionnaire = HttpContext.Session.Get<QuestionnaireModel>("Quest");
            questionOBJ = questionnaire.listQuestObj.FirstOrDefault(q => q.questionId == codeQuestion);
            return Json(new { Data = questionOBJ });
        }
        [HttpPost]
        public async Task<IActionResult> AddQuestionObj(string query)
        {
            QuestionnaireModel questionnaire = new QuestionnaireModel();
            QuestionOBJ questionOBJ = new QuestionOBJ();
            
            questionnaire = HttpContext.Session.Get<QuestionnaireModel>("Quest");
            questionOBJ = JsonConvert.DeserializeObject<QuestionOBJ>(query);
            questionnaire.listQuestObj.Add(questionOBJ);

            return Json(new { Data = questionOBJ });
        }
        // Passage de l'étape 0 à l'étape quest en mode modification, passage du questionnaire via le cookie de session
        [HttpGet]
        public async Task<IActionResult> ModifierQuestionnnaire(QuestionnaireModel questionnaireModel)
        {
            questionnaireModel= HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
            return View("Quest", questionnaireModel);
        }
        // Duplication du questionnaire sélectionné dans la list à l'étape 0
        [HttpPost]
        public IActionResult CopyQuestionnaireById(string query)
        {
            string Reponse = null;
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == int.Parse(query));
            questionnaire.IdQuestionnaire = null;
            _context.Questionnaire.Add(questionnaire);
            _context.SaveChanges();
            Reponse = "ok";
            return Json(new { Data = Reponse });
        }
        // Récupère le questionnaire en fonction de son id puis le place dans le cookie de session retourne l'url pour charger la vue quest en mode modification
        [HttpPost]
        public async Task<IActionResult> GetQuestionnaireById(string query)
        {
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == int.Parse(query));
            questionnaireModel.codeProduitFormation = questionnaire.CodeProduitFormation;
            questionnaireModel.dataJson = questionnaire.DataJson;
            questionnaireModel.description = questionnaire.Description;
            questionnaireModel.etatQuestionnaire = questionnaire.EtatQuestionnaire;
            questionnaireModel.questionnaireId = questionnaire.IdQuestionnaire;
            questionnaireModel.listQuestObj = JsonConvert.DeserializeObject<List<QuestionOBJ>>(questionnaire.DataJson);
            questionnaireModel.titreQuestionnaire = questionnaire.TitreQuestionnaire;
            HttpContext.Session.Set<QuestionnaireModel>("Questionnaire", questionnaireModel);
            return Json(new { result = "Redirect", url = Url.Action("ModifierQuestionnnaire", "OPS") });
        }
        // Récupère le code html du questionnaire
        [HttpPost]
        public async Task<IActionResult> PreviewQuestionnaireById(string query)
        {
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == int.Parse(query));
            questionnaireModel.codeHtml = questionnaire.CodeHtml;
            return Json(new { Data = questionnaireModel.codeHtml });
        }
        // Configuration du cookie non implémenté
        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }

        [HttpGet]
        public async Task<IActionResult> Identification(GestionEnquete gestionEnquete)
        {
            if (HttpContext.Session.Get<Etape0Model>("etape0") == null)
            {
                CollaborateurAfpa Formateur = _context.CollaborateurAfpa.Include(c => c.CodeTitreCiviliteNavigation).Where(c => c.MatriculeCollaborateurAfpa == "96GB011").First();
                gestionEnquete.
                BeneficiaireOffreFormation infosStagiaire = await _context.BeneficiaireOffreFormation
                    .Include(b => b.MatriculeBeneficiaireNavigation)
                    .Include(b => b.OffreFormationIdN)
                    //.ThenInclude(p=>p.CodeProduitFormationNavigation)
                    //.ThenInclude(t=>t.TitreProfessionnelN)
                    //.ThenInclude(cp=>cp.Ccp)
                    .Where(bo => bo.MatriculeBeneficiaire == "19071529"
                && bo.IdEtablissement == "19011" && bo.IdOffreFormation == 19017).SingleAsync();
                Beneficiaire beneficiaire = await _context.Beneficiaire.Include(b => b.CodeTitreCiviliteNavigation).Where(b => b.MatriculeBeneficiaire == "19071529").FirstAsync();
                etape0 = new Etape0Model
                {
                    IdOffreFormation = infosStagiaire.IdOffreFormation,
                    LibelleOffreFormation = infosStagiaire.OffreFormationIdN.LibelleOffreFormation,
                    PrenomNomBeneficiaire = infosStagiaire.MatriculeBeneficiaireNavigation.PrenomBeneficiaire + " " + infosStagiaire.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    MatriculeCollaborateurAfpa = infosStagiaire.OffreFormationIdN.MatriculeCollaborateurAfpa,
                    MatriculeBeneficiaire = infosStagiaire.MatriculeBeneficiaire,
                    NomBeneficiaire = infosStagiaire.MatriculeBeneficiaireNavigation.NomBeneficiaire,
                    PrenomBeneficiaire = infosStagiaire.MatriculeBeneficiaireNavigation.PrenomBeneficiaire,
                    TitreCivilitéBeneficiaire = beneficiaire.CodeTitreCiviliteNavigation.TitreCiviliteComplet,
                    DateDebutOffreFormation = infosStagiaire.OffreFormationIdN.DateDebutOffreFormation,
                    DateFinOffreFormation = infosStagiaire.OffreFormationIdN.DateFinOffreFormation,
                };
                HttpContext.Session.Set<Etape0Model>("etape0", etape0);
            }
            else
            {
                etape0 = HttpContext.Session.Get<Etape0Model>("etape0");
            }
            return View(etape0);
        }
        [HttpPost]
        public async Task<IActionResult> Identification(Etape0Model etape0, string BtnNext)
        {
            etape0 = HttpContext.Session.Get<Etape0Model>("etape0");
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.Set<Etape0Model>("etape0", etape0);

                }
            }
            return RedirectToAction("Competences");
        }

    }
}