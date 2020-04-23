using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class Pays : EntityBase
    {
        public Pays()
        {
            Entreprise = new HashSet<Entreprise>();
        }

        public string Idpays2 { get; set; }
        public string Idpays3 { get; set; }
        public int IdpaysNum { get; set; }
        public string LibellePays { get; set; }

        public virtual ICollection<Entreprise> Entreprise { get; set; }
    }
}
