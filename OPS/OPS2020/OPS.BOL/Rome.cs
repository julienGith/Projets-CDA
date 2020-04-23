using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class Rome
    {
        public Rome()
        {
            AppelationRome = new HashSet<AppelationRome>();
            ProduitFormationAppellationRome = new HashSet<ProduitFormationAppellationRome>();
        }

        public string CodeRome { get; set; }
        public string IntituleCodeRome { get; set; }
        public string CodeDomaineRome { get; set; }

        public virtual DomaineMetierRome CodeDomaineRomeNavigation { get; set; }
        public virtual ICollection<AppelationRome> AppelationRome { get; set; }
        public virtual ICollection<ProduitFormationAppellationRome> ProduitFormationAppellationRome { get; set; }
    }
}
