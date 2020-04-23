using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class ChampProfessionnnel : EntityBase
    {
        public ChampProfessionnnel()
        {
            UoChampProfessionnel = new HashSet<UoChampProfessionnel>();
        }

        public string IdChampProfessionnel { get; set; }
        public string LibelleChampProfessionnel { get; set; }

        public virtual ICollection<UoChampProfessionnel> UoChampProfessionnel { get; set; }
    }
}
