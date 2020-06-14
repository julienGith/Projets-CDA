using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class OffreFormation
    {
        public OffreFormation()
        {
            BeneficiaireOffreFormation = new HashSet<BeneficiaireOffreFormation>();
            CampagneMail = new HashSet<CampagneMail>();
        }

        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public string MatriculeCollaborateurAfpa { get; set; }
        public int CodeProduitFormation { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
        public DateTime DateDebutOffreFormation { get; set; }
        public DateTime DateFinOffreFormation { get; set; }

        public virtual ProduitFormation CodeProduitFormationNavigation { get; set; }
        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual CollaborateurAfpa MatriculeCollaborateurAfpaNavigation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormation { get; set; }
        public virtual ICollection<CampagneMail> CampagneMail { get; set; }
    }
}
