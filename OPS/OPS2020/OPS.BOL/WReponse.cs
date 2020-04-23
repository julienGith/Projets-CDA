using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class WReponse
    {
        public int IdQuestion { get; set; }
        public int IdQuestionnaire { get; set; }
        public Guid IdSoumissionaire { get; set; }
        public string RawValue { get; set; }
        public int EtatReponse { get; set; }
    }
}
