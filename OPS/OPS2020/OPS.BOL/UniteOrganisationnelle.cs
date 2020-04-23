using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class UniteOrganisationnelle
    {
        public UniteOrganisationnelle()
        {
            CollaborateurAfpa = new HashSet<CollaborateurAfpa>();
            UoChampProfessionnel = new HashSet<UoChampProfessionnel>();
        }

        public string Uo { get; set; }
        public string LibelleUo { get; set; }

        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpa { get; set; }
        public virtual ICollection<UoChampProfessionnel> UoChampProfessionnel { get; set; }
    }
}
