using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class UoChampProfessionnel
    {
        public string Uo { get; set; }
        public string IdChampProfessionnel { get; set; }

        public virtual ChampProfessionnnel IdChampProfessionnelNavigation { get; set; }
        public virtual UniteOrganisationnelle UoNavigation { get; set; }
    }
}
