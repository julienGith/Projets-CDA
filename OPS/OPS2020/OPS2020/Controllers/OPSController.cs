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
using System.Security.Cryptography;

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
                questionnaireModel.LibelleProduitFormation = etape0.LibelleProduitFormation;
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
        public async Task<IActionResult> ModifierQuestionnnaire(Etape0Model etape0, QuestionnaireModel questionnaireModel)
        {
            questionnaireModel = HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
            if (HttpContext.Session.Get<Etape0Model>("etape0") != null)
            {
                etape0 = HttpContext.Session.Get<Etape0Model>("etape0");
                questionnaireModel.codeProduitFormation = etape0.CodeProduitFormation;
                questionnaireModel.LibelleProduitFormation = etape0.LibelleProduitFormation;
            }
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
        public async Task<IActionResult> GetQuestionnaireById(string query,string mode)
        {
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == int.Parse(query));
            questionnaireModel.codeHtml = questionnaire.CodeHtml;
            questionnaireModel.codeProduitFormation = questionnaire.CodeProduitFormation;
            questionnaireModel.dataJson = questionnaire.DataJson;
            questionnaireModel.description = questionnaire.Description;
            questionnaireModel.etatQuestionnaire = questionnaire.EtatQuestionnaire;
            questionnaireModel.questionnaireId = questionnaire.IdQuestionnaire;
            questionnaireModel.listQuestObj = JsonConvert.DeserializeObject<List<QuestionOBJ>>(questionnaire.DataJson);
            questionnaireModel.titreQuestionnaire = questionnaire.TitreQuestionnaire;
            HttpContext.Session.Set<QuestionnaireModel>("Questionnaire", questionnaireModel);
            if (mode == "test")
            {
                return Json(new { result = "Redirect", url = Url.Action("TestQuest", "OPS") });
            }
            else
            {
                return Json(new { result = "Redirect", url = Url.Action("ModifierQuestionnnaire", "OPS") });
            }
            
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
        // GESTION ENQUÊTES
        // Récupère la liste des offres de formation en fonction du matricule du formateur
        [HttpGet]
        public async Task<IActionResult> GestionEnquete(GestionEnqueteModel gestionEnqueteModel)
        {
            List<OffreFormation> offreFormations = new List<OffreFormation>();
            List<CampagneMail> CampagneMails = new List<CampagneMail>();
            offreFormations = _context.OffreFormation.Include(o => o.CampagneMail).ThenInclude(o => o.PlanificationCampagneMail).Where(c => c.MatriculeCollaborateurAfpa == "96GB011").ToList();
            offreFormations.Reverse();
            foreach (var item in offreFormations)
            {
                OffreFormationModel offreFormationModel = new OffreFormationModel();
                offreFormationModel.IdOffreFormation = item.IdOffreFormation;
                offreFormationModel.IdEtablissement = item.IdEtablissement;
                offreFormationModel.BeneficiaireOffreFormation = item.BeneficiaireOffreFormation;
                offreFormationModel.CodeProduitFormation = item.CodeProduitFormation;
                offreFormationModel.DateFinOffreFormation = item.DateFinOffreFormation;
                offreFormationModel.LibelleOffreFormation = item.LibelleOffreFormation;
                offreFormationModel.LibelleReduitOffreFormation = item.LibelleReduitOffreFormation;
                foreach (var campagne in item.CampagneMail)
                {
                    CampagneMailModel campagneMailModel = new CampagneMailModel();
                    campagneMailModel.Description = campagne.Description;
                    campagneMailModel.IdEtablissement = campagne.IdEtablissement;
                    campagneMailModel.IdCampagneMail = campagne.IdCampagneMail;
                    campagneMailModel.IdOffreFormation = campagne.IdOffreFormation;
                    campagneMailModel.IdQuestionnaire = campagne.IdQuestionnaire;
                    foreach (var plan in campagne.PlanificationCampagneMail)
                    {
                        PlanificationCampagneMailModel planificationCampagneMailModel = new PlanificationCampagneMailModel();
                        planificationCampagneMailModel.IdCampagneMail = plan.IdCampagneMail;
                        planificationCampagneMailModel.DateRealisation = plan.DateRealisation;
                        campagneMailModel.PlanificationCampagneMailModel.Add(planificationCampagneMailModel);
                    }
                    offreFormationModel.CampagneMailModel.Add(campagneMailModel);
                }

                gestionEnqueteModel.OffreFormationModels.Add(offreFormationModel);
                HttpContext.Session.Set<GestionEnqueteModel>("gestionEnqueteModel", gestionEnqueteModel);
            }
            return View("gestionEnquete", gestionEnqueteModel);
        }
        // Récupère la liste des bénéficiaires en fontion de l'offre et de l'établissement
        [HttpPost]
        public async Task<IActionResult> GetListBeneficiaire(GestionEnqueteModel gestionEnqueteModel, string idOffreFormation, string idEtablissement)
        {
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("gestionEnqueteModel");
            OffreFormation offreFormation = new OffreFormation();
            offreFormation = await _context.OffreFormation.Where(c => c.IdEtablissement == idEtablissement && c.IdOffreFormation == int.Parse(idOffreFormation)).Include(o => o.BeneficiaireOffreFormation).ThenInclude(b => b.MatriculeBeneficiaireNavigation).ThenInclude(b => b.CodeTitreCiviliteNavigation).FirstAsync();
            foreach (BeneficiaireOffreFormation item in offreFormation.BeneficiaireOffreFormation)
            {
                BeneficiaireModel beneficiaireModel = new BeneficiaireModel();
                beneficiaireModel.MailBeneficiaire = item.MatriculeBeneficiaireNavigation.MailBeneficiaire;
                beneficiaireModel.MatriculeBeneficiaire = item.MatriculeBeneficiaireNavigation.MatriculeBeneficiaire;
                beneficiaireModel.NomBeneficiaire = item.MatriculeBeneficiaireNavigation.NomBeneficiaire;
                beneficiaireModel.PrenomBeneficiaire = item.MatriculeBeneficiaireNavigation.PrenomBeneficiaire;
                beneficiaireModel.TitreCivilite = item.MatriculeBeneficiaireNavigation.CodeTitreCiviliteNavigation.TitreCiviliteComplet;
                gestionEnqueteModel.BeneficiaireModels.Add(beneficiaireModel);
            }
            gestionEnqueteModel.OffreFormationModel.CodeProduitFormation = offreFormation.CodeProduitFormation;
            gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation = offreFormation.DateFinOffreFormation;
            gestionEnqueteModel.OffreFormationModel.IdEtablissement = offreFormation.IdEtablissement;
            gestionEnqueteModel.OffreFormationModel.IdOffreFormation = offreFormation.IdOffreFormation;
            HttpContext.Session.Set<GestionEnqueteModel>("GestionEnqueteModel", gestionEnqueteModel);
            return Json(new { result = "Redirect", url = Url.Action("CampagneEtape1", "OPS") });
        }

        [HttpGet]
        public async Task<IActionResult> CampagneEtape1(GestionEnqueteModel gestionEnqueteModel)
        {
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("GestionEnqueteModel");
            return View("CampagneMailEtape1", gestionEnqueteModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditListBeneficiaire(string matriculeBeneficiaire)
        {
            GestionEnqueteModel gestionEnqueteModel = new GestionEnqueteModel();
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("GestionEnqueteModel");
            gestionEnqueteModel.BeneficiaireModels.Remove(gestionEnqueteModel.BeneficiaireModels.FirstOrDefault(b => b.MatriculeBeneficiaire == matriculeBeneficiaire));
            HttpContext.Session.Set<GestionEnqueteModel>("GestionEnqueteModel", gestionEnqueteModel);
            return Json(new { result = "OK" });
        }
        [HttpGet]
        public async Task<IActionResult> CampagneEtape2(GestionEnqueteModel gestionEnqueteModel)
        {
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("GestionEnqueteModel");

            return View("CampagneMailEtape2", gestionEnqueteModel);
        }
        [HttpPost]
        public async Task<IActionResult> GetListQuestionnaire(GestionEnqueteModel gestionEnqueteModel, string description)
        {
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("GestionEnqueteModel");
            List<Questionnaire> questionnaires = new List<Questionnaire>();
            questionnaires = await _context.Questionnaire.Where(q => q.CodeProduitFormation == gestionEnqueteModel.OffreFormationModel.CodeProduitFormation).ToListAsync();
            foreach (var item in questionnaires)
            {
                QuestionnaireModel questionnaireModel = new QuestionnaireModel();
                questionnaireModel.titreQuestionnaire = item.TitreQuestionnaire;
                questionnaireModel.questionnaireId = item.IdQuestionnaire;
                gestionEnqueteModel.QuestionnaireModels.Add(questionnaireModel);
            }
            gestionEnqueteModel.CampagneMailModel.Description = description;
            HttpContext.Session.Set<GestionEnqueteModel>("GestionEnqueteModel", gestionEnqueteModel);

            return Json(new { result = "Redirect", url = Url.Action("CampagneEtape2", "OPS") });
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlanicationCampagne(GestionEnqueteModel gestionEnqueteModel, string idQuestionnaire)
        {
            gestionEnqueteModel = HttpContext.Session.Get<GestionEnqueteModel>("GestionEnqueteModel");
            CampagneMail campagneMail = new CampagneMail();
            campagneMail.IdQuestionnaire = int.Parse(idQuestionnaire);
            campagneMail.IdEtablissement = gestionEnqueteModel.OffreFormationModel.IdEtablissement;
            campagneMail.IdOffreFormation = gestionEnqueteModel.OffreFormationModel.IdOffreFormation;
            campagneMail.DateCreation = DateTime.Today.Date;
            campagneMail.Description = gestionEnqueteModel.CampagneMailModel.Description;
            _context.CampagneMail.Add(campagneMail);
            _context.SaveChanges();
            int idCampagneMail = _context.CampagneMail.Where(c => c.IdEtablissement == gestionEnqueteModel.OffreFormationModel.IdEtablissement && c.IdOffreFormation == gestionEnqueteModel.OffreFormationModel.IdOffreFormation)
                .Select(c => c.IdCampagneMail).FirstOrDefault();
            //Fin de formation
            PlanificationCampagneMail planificationCampagneMail1 = new PlanificationCampagneMail();
            if (DateTime.Today > gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation)
            {
                planificationCampagneMail1.DateRealisation = DateTime.Today;
                planificationCampagneMail1.Echeance = DateTime.Today;
            }
            else
            {
                planificationCampagneMail1.DateRealisation = gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddDays(1);
                planificationCampagneMail1.Echeance = gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddDays(1);
            }
            foreach (var item in gestionEnqueteModel.BeneficiaireModels)
            {
                DestinataireEnquete destinataireEnquete = new DestinataireEnquete();
                destinataireEnquete.MatriculeBeneficiaire = item.MatriculeBeneficiaire;
                destinataireEnquete.IdEtablissement = gestionEnqueteModel.OffreFormationModel.IdEtablissement;
                destinataireEnquete.IdOffreFormation = gestionEnqueteModel.OffreFormationModel.IdOffreFormation;
                destinataireEnquete.DateRealisationEnquete = null;

                destinataireEnquete.DateEcheanceEnquete = planificationCampagneMail1.Echeance;
                destinataireEnquete.DateRelance1 = null;
                destinataireEnquete.DateRelance2 = null;
                destinataireEnquete.EtapeQuestionnaire = -1;
                destinataireEnquete.EtatTraitementQuestionnaire = 0;
                destinataireEnquete.EnEmploi = null;
                destinataireEnquete.IdContrat = null;
                destinataireEnquete.IdSoumissionnaire = Guid.NewGuid();
                planificationCampagneMail1.DestinataireEnquete.Add(destinataireEnquete);
            }
            planificationCampagneMail1.Type = "Fin";
            planificationCampagneMail1.EtatTraitement = 0;
            planificationCampagneMail1.IdCampagneMail = idCampagneMail;
            planificationCampagneMail1.NombreDestinataires = gestionEnqueteModel.BeneficiaireModels.Count();
            planificationCampagneMail1.NombreReponses = 0;
            planificationCampagneMail1.NombreEnvois = 0;


            //A 6 mois
            PlanificationCampagneMail planificationCampagneMail2 = new PlanificationCampagneMail();
            if (DateTime.Today > gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(6) && DateTime.Today < gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(12))
            {
                planificationCampagneMail2.DateRealisation = DateTime.Today;
                planificationCampagneMail2.Echeance = DateTime.Today;
            }
            else
            {
                planificationCampagneMail2.DateRealisation = gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(6);
                planificationCampagneMail2.Echeance = gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(6);
            }
            foreach (var item in gestionEnqueteModel.BeneficiaireModels)
            {
                DestinataireEnquete destinataireEnquete = new DestinataireEnquete();
                destinataireEnquete.MatriculeBeneficiaire = item.MatriculeBeneficiaire;
                destinataireEnquete.IdEtablissement = gestionEnqueteModel.OffreFormationModel.IdEtablissement;
                destinataireEnquete.IdOffreFormation = gestionEnqueteModel.OffreFormationModel.IdOffreFormation;
                destinataireEnquete.DateRealisationEnquete = null;
                destinataireEnquete.DateEcheanceEnquete = planificationCampagneMail2.Echeance;
                destinataireEnquete.DateRelance1 = null;
                destinataireEnquete.DateRelance2 = null;
                destinataireEnquete.EtapeQuestionnaire = -1;
                destinataireEnquete.EtatTraitementQuestionnaire = 0;
                destinataireEnquete.EnEmploi = null;
                destinataireEnquete.IdContrat = null;
                destinataireEnquete.IdSoumissionnaire = Guid.NewGuid();
                planificationCampagneMail2.DestinataireEnquete.Add(destinataireEnquete);
            }
            planificationCampagneMail2.Type = "A6mois";
            planificationCampagneMail2.EtatTraitement = 0;
            planificationCampagneMail2.IdCampagneMail = idCampagneMail;
            planificationCampagneMail2.NombreDestinataires = gestionEnqueteModel.BeneficiaireModels.Count();
            planificationCampagneMail2.NombreReponses = 0;
            planificationCampagneMail2.NombreEnvois = 0;
            planificationCampagneMail2.Echeance = planificationCampagneMail2.Echeance;

            //A 12 mois
            PlanificationCampagneMail planificationCampagneMail3 = new PlanificationCampagneMail();
            if (DateTime.Today > gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation && DateTime.Today < gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(12))
            {
                planificationCampagneMail3.DateRealisation = DateTime.Today;
                planificationCampagneMail3.Echeance = DateTime.Today;
            }
            else
            {
                planificationCampagneMail3.DateRealisation = gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(12);
                planificationCampagneMail3.Echeance = gestionEnqueteModel.OffreFormationModel.DateFinOffreFormation.AddMonths(12);
            }
            foreach (var item in gestionEnqueteModel.BeneficiaireModels)
            {
                DestinataireEnquete destinataireEnquete = new DestinataireEnquete();
                destinataireEnquete.MatriculeBeneficiaire = item.MatriculeBeneficiaire;
                destinataireEnquete.IdEtablissement = gestionEnqueteModel.OffreFormationModel.IdEtablissement;
                destinataireEnquete.IdOffreFormation = gestionEnqueteModel.OffreFormationModel.IdOffreFormation;
                destinataireEnquete.DateRealisationEnquete = null;
                destinataireEnquete.DateEcheanceEnquete = planificationCampagneMail3.Echeance;
                destinataireEnquete.DateRelance1 = null;
                destinataireEnquete.DateRelance2 = null;
                destinataireEnquete.EtapeQuestionnaire = -1;
                destinataireEnquete.EtatTraitementQuestionnaire = 0;
                destinataireEnquete.EnEmploi = null;
                destinataireEnquete.IdContrat = null;
                destinataireEnquete.IdSoumissionnaire = Guid.NewGuid();
                planificationCampagneMail3.DestinataireEnquete.Add(destinataireEnquete);
            }
            planificationCampagneMail3.Type = "A12mois";
            planificationCampagneMail3.EtatTraitement = 0;
            planificationCampagneMail3.IdCampagneMail = idCampagneMail;
            planificationCampagneMail3.NombreDestinataires = gestionEnqueteModel.BeneficiaireModels.Count();
            planificationCampagneMail3.NombreReponses = 0;
            planificationCampagneMail3.NombreEnvois = 0;
            planificationCampagneMail3.Echeance = planificationCampagneMail3.Echeance;

            _context.PlanificationCampagneMail.AddRange(planificationCampagneMail1, planificationCampagneMail2, planificationCampagneMail3);
            _context.SaveChanges();
            return Json(new { result = "OK" });
        }

        //[HttpPost]
        //public async Task<IActionResult> TestQuest(QuestionnaireModel questionnaireModel)
        //{
        //    questionnaireModel = HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
        //    return View("TestQuest", questionnaireModel);
        //}
        [HttpGet]
        public async Task<IActionResult> TestQuest(Etape0Model etape0, QuestionnaireModel questionnaireModel)
        {
            questionnaireModel = HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
            if (HttpContext.Session.Get<Etape0Model>("etape0") != null)
            {
                etape0 = HttpContext.Session.Get<Etape0Model>("etape0");
                questionnaireModel.codeProduitFormation = etape0.CodeProduitFormation;
                questionnaireModel.LibelleProduitFormation = etape0.LibelleProduitFormation;
            }
            return View("TestQuest", questionnaireModel);
        }

    }

}