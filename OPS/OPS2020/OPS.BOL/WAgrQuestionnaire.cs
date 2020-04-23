using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class WAgrQuestionnaire
    {
        public int IdQuestionnaire { get; set; }
        public string Type { get; set; }
        public string NumOrdre { get; set; }
        public int NombreEnvois { get; set; }
        public int NombreReponses { get; set; }
        public string Valeurs { get; set; }
    }
}
