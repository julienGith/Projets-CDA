using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPS2020.Models
{

    public class QuestionnaireModel
    {
        public class QuestionModel
        {
            public int IdQuestion { get; set; }
            public int Ordre { get; set; }
            public string Type { get; set; }
            public string CodeHTML { get; set; }
            public string Reponse { get; set; }
        }
        public int IdQuestionnaire { get; set; }
        public string TitreQuestionnaire { get; set; }
        public string Description { get; set; }
        public string DataJson { get; set; }
        public int CodeProduitFormation { get; set; }
        public int EtatQuestionnaire { get; set; }
        List<QuestionModel> questions = new List<QuestionModel>();
    }
}
