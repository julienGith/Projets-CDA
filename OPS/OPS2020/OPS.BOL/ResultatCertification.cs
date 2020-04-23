using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class ResultatCertification
    {
        public ResultatCertification()
        {
            BeneficiaireOffreFormation = new HashSet<BeneficiaireOffreFormation>();
        }

        public string CodeResultatCertification { get; set; }
        public string LibelleResultatCertification { get; set; }

        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormation { get; set; }
    }
}
