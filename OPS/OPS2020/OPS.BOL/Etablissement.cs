using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class Etablissement
    {
        public Etablissement()
        {
            CollaborateurAfpa = new HashSet<CollaborateurAfpa>();
            InverseIdEtablissementRattachementNavigation = new HashSet<Etablissement>();
            OffreFormation = new HashSet<OffreFormation>();
        }

        public string IdEtablissement { get; set; }
        public string IdEtablissementRattachement { get; set; }
        public string NomEtablissement { get; set; }
        public string MailEtablissement { get; set; }
        public string TelEtablissement { get; set; }
        public string Ligne1Adresse { get; set; }
        public string Ligne2Adresse { get; set; }
        public string Ligne3Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }

        public virtual Etablissement IdEtablissementRattachementNavigation { get; set; }
        public virtual ICollection<CollaborateurAfpa> CollaborateurAfpa { get; set; }
        public virtual ICollection<Etablissement> InverseIdEtablissementRattachementNavigation { get; set; }
        public virtual ICollection<OffreFormation> OffreFormation { get; set; }
    }
}
