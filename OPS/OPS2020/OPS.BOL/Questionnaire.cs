using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class Questionnaire
    {
        public int? IdQuestionnaire { get; set; }
        public string TitreQuestionnaire { get; set; }
        public string Description { get; set; }
        public string DataJson { get; set; }
        public string CodeHtml { get; set; }
        public int CodeProduitFormation { get; set; }
        public int EtatQuestionnaire { get; set; }

        public virtual ProduitFormation CodeProduitFormationNavigation { get; set; }
    }
}
