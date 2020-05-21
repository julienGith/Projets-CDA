using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPS.BOL;

namespace OPS2020.Models
{

    public class Etape0Model
    {
        public int CodeProduitFormation { get; set; }
        public string IdQuestionnaire { get; set; }
    }
    public class FormateurModel
    {
        public string MatriculeCollaborateurAfpa { get; set; }
        public string TitreCivilite { get; set; }
        public string NomCollaborateur { get; set; }
        public string PrenomCollaborateur { get; set; }
        public string MailCollaborateurAfpa { get; set; }
        public virtual ICollection<OffreFormationModel> OffreFormations { get; set; }

    }
    public class OffreFormationModel
    {
        public int CodeProduitFormation { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
        public DateTime DateFinOffreFormation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormation { get; set; }
        public virtual ICollection<CampagneMail> CampagneMail { get; set; }
    }
    public class GestionEnqueteModel
    {
        public GestionEnqueteModel()
        {
            OffreFormationModels = new HashSet<OffreFormationModel>();
        }
        public virtual ICollection<OffreFormationModel> OffreFormationModels { get; set; }
    }

}
