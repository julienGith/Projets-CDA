﻿using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class PlanificationCampagneMail : EntityBase
    {
        public PlanificationCampagneMail()
        {
            DestinataireEnquete = new HashSet<DestinataireEnquete>();
        }

        public int IdPlanificationCampagneMail { get; set; }
        public int IdCampagneMail { get; set; }
        public string Type { get; set; }
        public DateTime Echeance { get; set; }
        public DateTime? DateRealisation { get; set; }
        public int NombreDestinataires { get; set; }
        public int NombreEnvois { get; set; }
        public int NombreReponses { get; set; }
        public int EtatTraitement { get; set; }

        public virtual CampagneMail IdCampagneMailNavigation { get; set; }
        public virtual ICollection<DestinataireEnquete> DestinataireEnquete { get; set; }
    }
}
