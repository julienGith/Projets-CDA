using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class DomaineMetierRome
    {
        public DomaineMetierRome()
        {
            Rome = new HashSet<Rome>();
        }

        public string CodeDomaineRome { get; set; }
        public string IntituleDomaineRome { get; set; }
        public string CodeFamilleRome { get; set; }

        public virtual FamilleMetierRome CodeFamilleRomeNavigation { get; set; }
        public virtual ICollection<Rome> Rome { get; set; }
    }
}
