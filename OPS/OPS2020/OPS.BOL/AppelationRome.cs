using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class AppelationRome : EntityBase
    {
        public int CodeAppelationRome { get; set; }
        public string LibelleAppelationRome { get; set; }
        public string CodeRome { get; set; }

        public virtual Rome CodeRomeNavigation { get; set; }
    }
   
    }
