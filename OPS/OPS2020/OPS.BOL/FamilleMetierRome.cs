using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class FamilleMetierRome : EntityBase
    {
        public FamilleMetierRome()
        {
            DomaineMetierRome = new HashSet<DomaineMetierRome>();
        }

        public string CodeFamilleMetierRome { get; set; }
        public string IntituleFamilleMetierRome { get; set; }

        public virtual ICollection<DomaineMetierRome> DomaineMetierRome { get; set; }
    }
}
