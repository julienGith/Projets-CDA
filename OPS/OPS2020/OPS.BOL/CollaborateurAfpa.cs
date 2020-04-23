using System;
using System.Collections.Generic;

namespace OPS.BOL
{
    public partial class CollaborateurAfpa : EntityBase
    {
        public CollaborateurAfpa()
        {
            OffreFormation = new HashSet<OffreFormation>();
        }

        public string MatriculeCollaborateurAfpa { get; set; }
        public string IdEtablissement { get; set; }
        public int CodeTitreCivilite { get; set; }
        public string NomCollaborateur { get; set; }
        public string PrenomCollaborateur { get; set; }
        public string MailCollaborateurAfpa { get; set; }
        public string TelCollaborateurAfpa { get; set; }
        public int? IdFonction { get; set; }
        public string Uo { get; set; }
        public string UserId { get; set; }

        public virtual TitreCivilite CodeTitreCiviliteNavigation { get; set; }
        public virtual Etablissement IdEtablissementNavigation { get; set; }
        public virtual TypeFonction IdFonctionNavigation { get; set; }
        public virtual UniteOrganisationnelle UoNavigation { get; set; }
        public virtual ICollection<OffreFormation> OffreFormation { get; set; }
    }
}
