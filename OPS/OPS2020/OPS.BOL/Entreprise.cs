using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class Entreprise : EntityBase
    {
        public Entreprise()
        {
            Contrat = new HashSet<Contrat>();
        }

        public int IdEntreprise { get; set; }
        public string RaisonSociale { get; set; }
        public string NumeroSiret { get; set; }
        public string MailEntreprise { get; set; }
        public string TelEntreprise { get; set; }
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Idpays2 { get; set; }

        public virtual Pays Idpays2Navigation { get; set; }
        public virtual ICollection<Contrat> Contrat { get; set; }
    }
}
