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
        [HttpPost]
        public void CreerQuestionnaire(string data)
        {
            //foreach (var item in data["listQuestObj"])
            //{
            //}
            //SetCookie("Questionnaire", questionnaireJson, 180);
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaireModel = JsonConvert.DeserializeObject<QuestionnaireModel>(data);
            if (questionnaireModel.questionnaireId != null)
            {
                questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == questionnaireModel.questionnaireId);
                questionnaire.CodeProduitFormation = questionnaireModel.codeProduitFormation;
                questionnaire.DataJson = JsonConvert.SerializeObject(questionnaireModel.listQuestObj);
                questionnaire.Description = questionnaireModel.description;
                questionnaire.EtatQuestionnaire = questionnaireModel.etatQuestionnaire;
                questionnaire.TitreQuestionnaire = questionnaireModel.titreQuestionnaire;
            }
            else
            {
                questionnaire.CodeProduitFormation = questionnaireModel.codeProduitFormation;
                questionnaire.DataJson = JsonConvert.SerializeObject(questionnaireModel.listQuestObj);
                questionnaire.Description = questionnaireModel.description;
                questionnaire.EtatQuestionnaire = questionnaireModel.etatQuestionnaire;
                questionnaire.TitreQuestionnaire = questionnaireModel.titreQuestionnaire;
                _context.Questionnaire.Add(questionnaire);
            }

            _context.SaveChanges();
        }
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
        [HttpGet]
        public async Task<IActionResult> ModifierQuestionnnaire(QuestionnaireModel questionnaireModel)
        {
            questionnaireModel= HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
            return View("Quest", questionnaireModel);
        }
        [HttpGet]
        public async Task<IActionResult> PreviewQuestionnnaire(QuestionnaireModel questionnaireModel)
        {
            questionnaireModel = HttpContext.Session.Get<QuestionnaireModel>("Questionnaire");
            return View("Quest", questionnaireModel);
        }
        [HttpPost]
        public void CopyQuestionnaireById(string query)
        {
            QuestionnaireModel questionnaireModel = new QuestionnaireModel();
            Questionnaire questionnaire = new Questionnaire();
            questionnaire = _context.Questionnaire.FirstOrDefault(q => q.IdQuestionnaire == int.Parse(query));
            questionnaire.IdQuestionnaire = null;
            _context.Questionnaire.Add(questionnaire);
            _context.SaveChanges();
            //return Json(new { result = "Redirect", url = Url.Action("ModifierQuestionnnaire", "OPS") });
        }
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
        [HttpPost]
        public async Task<IActionResult> PreviewQuestionnaireById(string query)
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
            return Json(new { result = "Redirect", url = Url.Action("PreviewQuestionnnaire", "OPS") });
        }

        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }
    }
}