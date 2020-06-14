using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class ProduitFormation
    {
        public ProduitFormation()
        {
            OffreFormation = new HashSet<OffreFormation>();
            ProduitFormationAppellationRome = new HashSet<ProduitFormationAppellationRome>();
            Questionnaire = new HashSet<Questionnaire>();
        }

        public int CodeProduitFormation { get; set; }
        public string NiveauFormation { get; set; }
        public string LibelleProduitFormation { get; set; }
        public string LibelleCourtFormation { get; set; }
        public bool FormationContinue { get; set; }
        public bool FormationDiplomante { get; set; }

        public virtual ICollection<OffreFormation> OffreFormation { get; set; }
        public virtual ICollection<ProduitFormationAppellationRome> ProduitFormationAppellationRome { get; set; }
        public virtual ICollection<Questionnaire> Questionnaire { get; set; }
    }
}
