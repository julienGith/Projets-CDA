using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPS2020.Models
{
    public class Item
    {

    }
    public class QuestionOBJ
    {
        public string type { get; set; }
        public string sousType { get; set; }
        public string choixRequis { get; set; }
        public string questionId { get; set; }
        public string intitule { get; set; }
        public string dataTarget { get; set; }
        public string TargetId { get; set; }
        public string intituleTxtBId { get; set; }
        public string intituleDivBId { get; set; }
        public string requisOuiRadioId { get; set; }
        public string requisNonRadioId { get; set; }
        public string requisRadioName { get; set; }
        public string requis { get; set; }
        public string ddlId { get; set; }
        public string ajoutSupprChoix { get; set; }
        public string ajouterBtnId { get; set; }
        public string supprBtnId { get; set; }
        public string trashIconId { get; set; }
        public string graphique { get; set; }
        public string graphiqueId { get; set; }
        public string graphiqueLigneId { get; set; }
        public string graphiqueColId { get; set; }
        public string questionPreviewId { get; set; }
        public string divPosPrev { get; set; }
        public string codeHtml { get; set; }
        public string itemUlId { get; set; }
        public string itemLiId { get; set; }
        List<Item> items = new List<Item>();
    }
    public class QuestionModel
    {
        public int IdQuestion { get; set; }
        public int Ordre { get; set; }
        public string Type { get; set; }
        public string CodeHTML { get; set; }
        public string Reponse { get; set; }
    }
    public class QuestionnaireModel
    {
        public QuestionnaireModel()
        {
            questions = new HashSet<QuestionModel>();
            questionsObj = new HashSet<QuestionOBJ>();
        }
        public int IdQuestionnaire { get; set; }
        public string TitreQuestionnaire { get; set; }
        public string Description { get; set; }
        public string DataJson { get; set; }
        public int CodeProduitFormation { get; set; }
        public int EtatQuestionnaire { get; set; }
        public ICollection<QuestionModel> questions { get; set; }
        public ICollection<QuestionOBJ> questionsObj { get; set; }
    }
}
