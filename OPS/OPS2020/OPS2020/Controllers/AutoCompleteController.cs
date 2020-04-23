using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPS.BOL;
using OPS.DAL;
using OPS2020.Models;

namespace OPS2020.Controllers
{
    public class AutoCompleteController : Controller
    {
        private readonly OPSCtx _context;
        public AutoCompleteController(OPSCtx context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> LibelleProduitFormation(string query)
        {

            return await AutoCompleteSqlServer(query);

        }
        [HttpPost]
        public async Task<IActionResult> SigleProduitFormation(string query)
        {
            return await AutoCompleteAvecListe(query);
        }
        [HttpPost]
        public  IActionResult SigleProduitFormationByLibelle(string query)
        {
            string Reponse = null;
            ProduitFormation produit = new ProduitFormation();
            produit =  _context.ProduitFormation.FirstOrDefault(p => p.LibelleProduitFormation.ToLower().Contains(query.ToLower()));
            Reponse = produit.LibelleCourtFormation.ToString();
            return Json(new { Data = Reponse });
        }
        [HttpPost]
        public IActionResult LibelleProduitFormationBySigle(string query)
        {
            string Reponse = null;
            ProduitFormation produit = new ProduitFormation();
            produit = _context.ProduitFormation.FirstOrDefault(p => p.LibelleCourtFormation.ToLower().Contains(query.ToLower()));
            Reponse = produit.LibelleProduitFormation.ToString();
            return Json(new { Data = Reponse });
        }
        [HttpPost]
        public IActionResult GetProduitFormation(string query,Etape0Model etape0)
        {
            ProduitFormation produit = new ProduitFormation();
            produit = _context.ProduitFormation.FirstOrDefault(p => p.LibelleCourtFormation.ToLower().Contains(query.ToLower()));
            etape0.CodeProduitFormation = produit.CodeProduitFormation;
            HttpContext.Session.Set<Etape0Model>("etape0", etape0);
            return Json(new { Data = produit });
        }
        [HttpPost]
        public async Task<IActionResult> GetListQuestionnaire(int query)
        {
            List<Questionnaire> questionnaires = new List<Questionnaire>();
            questionnaires = await _context.Questionnaire.Where(q => q.CodeProduitFormation == query).ToListAsync();
            return Json(new { Data = questionnaires });
        }
        private async Task<IActionResult> AutoCompleteSqlServer(string query)
        {
            List<string> listReponse = null;
            listReponse = await _context.ProduitFormation.Select(p => p.LibelleProduitFormation).Where(C => C.ToLower().StartsWith(query.ToLower())).ToListAsync();
            return Json(new { Data = listReponse });
        }

        private async Task<IActionResult> AutoCompleteAvecListe(string query)
        {
            List<string> listReponse = null;
            List<string> listData = null;
            listData = await _context.ProduitFormation.Select(p => p.LibelleCourtFormation).ToListAsync();
            listReponse = listData.Where(C => C.ToLower().StartsWith(query.ToLower())).ToList();
            return Json(new { Data = listReponse });
        }
    }
}