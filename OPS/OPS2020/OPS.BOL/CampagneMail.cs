using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class CampagneMail : EntityBase
    {
        public CampagneMail()
        {
            PlanificationCampagneMail = new HashSet<PlanificationCampagneMail>();
        }

        public int IdCampagneMail { get; set; }
        public int IdQuestionnaire { get; set; }
        public DateTime DateCreation { get; set; }
        public string Description { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }

        public virtual OffreFormation Id { get; set; }
        public virtual ICollection<PlanificationCampagneMail> PlanificationCampagneMail { get; set; }
    }
}
