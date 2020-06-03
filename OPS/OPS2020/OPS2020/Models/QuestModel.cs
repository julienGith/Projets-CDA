using Newtonsoft.Json;
using OPS.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPS2020.Models
{

    public class UserItem
    {
        public string id { get; set; }
        public string idPreview { get; set; }
        public string txtBId { get; set; }
        public string ulId { get; set; }
        public string intitule { get; set; }
        public string codeHtml { get; set; }
        public string groupName { get; set; }
        [JsonProperty(PropertyName = "itemPrev")]
        public Item itemPrev { get; set; }
        public string type { get; set; }
    }
    public class Item
    {
        public string id { get; set; }
        public string lbl { get; set; }
        public string lblId { get; set; }
        public string Name { get; set; }
        public string intitule { get; set; }
        public string groupName { get; set; }
        public string codeHtml { get; set; }
    }
    public class QuestionOBJ
    {
        public int numQ { get; set; }
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
        public string questionPreviewId { get; set; }
        public string graphiqueLigneId { get; set; }
        public string graphiqueColId { get; set; }
        public string divPosPrev { get; set; }
        public string codeHtml { get; set; }
        public string itemUlId { get; set; }
        public string itemLiId { get; set; }
        public bool modifUser { get; set; }
        public string divListItemId { get; set; }
        [JsonProperty(PropertyName = "listUserItems")]
        public ICollection<UserItem> listUserItems { get; set; }
        [JsonProperty(PropertyName = "listItems")]
        public ICollection<Item> listItems { get; set; }

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
            listQuestObj = new HashSet<QuestionOBJ>();
        }
        public bool ModePreview { get; set; }
        public int? questionnaireId { get; set; }
        public string titreQuestionnaire { get; set; }
        public string description { get; set; }
        public string dataJson { get; set; }
        public string codeHtml { get; set; }
        public int codeProduitFormation { get; set; }
        public string LibelleProduitFormation { get; set; }
        public int etatQuestionnaire { get; set; }
        [JsonProperty(PropertyName = "listQuestObj")]
        public ICollection<QuestionOBJ> listQuestObj { get; set; }
    }
}
