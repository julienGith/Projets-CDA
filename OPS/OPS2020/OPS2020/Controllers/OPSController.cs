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
            if (HttpContext.Session.Get<Etape0Model>("etape0") != null)
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
            questionnaireModel = HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
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

        [HttpPost]
        public IActionResult GetListeOffreFormation(string query)
        {
            //matriculeFormateur = "96GB011";
            GestionEnqueteModel gestionEnqueteModel = new GestionEnqueteModel();
            List<OffreFormation> offreFormations = new List<OffreFormation>();
            List<CampagneMail> CampagneMails = new List<CampagneMail>();

            if (HttpContext.Session.Get<Etape0Model>("gestionEnqueteEtape1") == null)
            {
                offreFormations = _context.OffreFormation.Include(o => o.CampagneMail).ThenInclude(o => o.PlanificationCampagneMail).Where(c => c.MatriculeCollaborateurAfpa == "96GB011").ToList();
                foreach (var item in offreFormations)
                {
                    OffreFormationModel offreFormationModel = new OffreFormationModel();

                    offreFormationModel.BeneficiaireOffreFormation = item.BeneficiaireOffreFormation;
                    offreFormationModel.CodeProduitFormation = item.CodeProduitFormation;
                    offreFormationModel.DateFinOffreFormation = item.DateFinOffreFormation;
                    offreFormationModel.LibelleOffreFormation = item.LibelleOffreFormation;
                    offreFormationModel.LibelleReduitOffreFormation = item.LibelleReduitOffreFormation;
                    offreFormationModel.CampagneMail = item.CampagneMail;
                    gestionEnqueteModel.OffreFormationModels.Add(offreFormationModel);

                    //foreach (var campagne in item.CampagneMail)
                    //{
                    //    CampagneMailModel campagneMailModel = new CampagneMailModel();
                    //    campagneMailModel.DateCreation = campagne.DateCreation;
                    //    campagneMailModel.Description = campagne.Description;
                    //    campagneMailModel.IdCampagneMail = campagne.IdCampagneMail;
                    //    campagneMailModel.IdEtablissement = campagne.IdEtablissement;
                    //    campagneMailModel.IdOffreFormation = campagne.IdOffreFormation;
                    //    campagneMailModel.IdQuestionnaire = campagne.IdQuestionnaire;
                    //    offreFormationModel.CampagneMailModel.Add(campagneMailModel);

                    //    foreach (var planification in campagne.PlanificationCampagneMail)
                    //    {
                    //        PlanificationCampagneMailModel planificationCampagneMailModel = new PlanificationCampagneMailModel();
                    //        planificationCampagneMailModel.IdCampagneMail = planification.IdCampagneMail;
                    //        //planificationCampagneMailModel.IdCampagneMailNavigation = planification.IdCampagneMailNavigation;
                    //        planificationCampagneMailModel.IdPlanificationCampagneMail = planification.IdPlanificationCampagneMail;
                    //        planificationCampagneMailModel.NombreDestinataires = planification.NombreDestinataires;
                    //        planificationCampagneMailModel.NombreEnvois = planification.NombreEnvois;
                    //        planificationCampagneMailModel.NombreReponses = planification.NombreReponses;
                    //        planificationCampagneMailModel.Type = planification.Type;
                    //        campagneMailModel.PlanificationCampagneMailModel.Add(planificationCampagneMailModel);
                    //    }
                    //    campagneMailModel.IdQuestionnaire = campagne.IdQuestionnaire;
                    //}

                }
                //HttpContext.Session.Set<GestionEnqueteModel>("gestionEnqueteEtape1", gestionEnqueteModel);
            }

            return Json(new { Data = gestionEnqueteModel.OffreFormationModels });
        }
        [HttpGet]
        public async Task<IActionResult> GestionEnquete(GestionEnqueteModel gestionEnqueteModel)
        {
            //GestionEnqueteModel gestionEnqueteModel = new GestionEnqueteModel();
            List<OffreFormation> offreFormations = new List<OffreFormation>();
            List<CampagneMail> CampagneMails = new List<CampagneMail>();
            if (HttpContext.Session.Get<GestionEnqueteModel>("gestionEnquete") != null)
            {
                gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("gestionEnquete");
            }
            if (HttpContext.Session.Get<Etape0Model>("gestionEnqueteEtape1") == null)
            {
                offreFormations = _context.OffreFormation.Include(o => o.CampagneMail).ThenInclude(o => o.PlanificationCampagneMail).Where(c => c.MatriculeCollaborateurAfpa == "96GB011").ToList();
                offreFormations.Reverse();
                foreach (var item in offreFormations)
                {
                    OffreFormationModel offreFormationModel = new OffreFormationModel();

                    offreFormationModel.BeneficiaireOffreFormation = item.BeneficiaireOffreFormation;
                    offreFormationModel.CodeProduitFormation = item.CodeProduitFormation;
                    offreFormationModel.DateFinOffreFormation = item.DateFinOffreFormation;
                    offreFormationModel.LibelleOffreFormation = item.LibelleOffreFormation;
                    offreFormationModel.LibelleReduitOffreFormation = item.LibelleReduitOffreFormation;
                    offreFormationModel.CampagneMail = item.CampagneMail;
                    gestionEnqueteModel.OffreFormationModels.Add(offreFormationModel);


                }
                //HttpContext.Session.Set<GestionEnqueteModel>("gestionEnqueteEtape1", gestionEnqueteModel);
            }


            return View("gestionEnquete", gestionEnqueteModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetListBeneficiaire(string codeProduitFormation)
        {
            GestionEnqueteModel gestionEnqueteModel = new GestionEnqueteModel();
            OffreFormation offreFormation = new OffreFormation();
            BeneficiaireModel beneficiaireModel = new BeneficiaireModel();
            offreFormation = _context.OffreFormation.Where(c => c.CodeProduitFormation == int.Parse(codeProduitFormation)).Include(o => o.BeneficiaireOffreFormation).ThenInclude(b=>b.MatriculeBeneficiaireNavigation).ThenInclude(b=>b.CodeTitreCiviliteNavigation).First();
            foreach (var item in offreFormation.BeneficiaireOffreFormation)
            {
                beneficiaireModel.MailBeneficiaire = item.MatriculeBeneficiaireNavigation.MailBeneficiaire;
                beneficiaireModel.MatriculeBeneficiaire = item.MatriculeBeneficiaireNavigation.MatriculeBeneficiaire;
                beneficiaireModel.NomBeneficiaire = item.MatriculeBeneficiaireNavigation.NomBeneficiaire;
                beneficiaireModel.PrenomBeneficiaire = item.MatriculeBeneficiaireNavigation.PrenomBeneficiaire;
                beneficiaireModel.TitreCivilite = item.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet;

                gestionEnqueteModel.BeneficiaireModels.Add(beneficiaireModel);
            }
            HttpContext.Session.Set<GestionEnqueteModel>("GestionEnqueteModel", gestionEnqueteModel);
            return Json(new { result = "Redirect", url = Url.Action("CampagneEtape1", "OPS") });
        }

        [HttpGet]
        public async Task<IActionResult> CampagneEtape1(GestionEnqueteModel gestionEnqueteModel)
        {
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("GestionEnqueteModel");
            return View("CampagneMailEtape1", gestionEnqueteModel);
        }
    }

}