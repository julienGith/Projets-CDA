using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class TitreCivilite
    {
        public TitreCivilite()
        {
            Beneficiaire = new HashSet<Beneficiaire>();
            CollaborateurAfpa = new HashSet<CollaborateurAfpa>();
        }

        public int CodeTitreCivilite { get; set; }
        public string TitreCiviliteAbrege { get; set; }
        public string TitreCiviliteComplet { get; set; }

        public virtual ICollection<Beneficiaire> Beneficiaire { get; set; }
        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpa { get; set; }
    }
}
