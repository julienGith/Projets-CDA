using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class PlanificationCampagneMailModel
    {
        public PlanificationCampagneMailModel()
        {
            DestinataireEnquete = new HashSet<DestinataireEnquete>();
        }

        public int IdPlanificationCampagneMail { get; set; }
        public int IdCampagneMail { get; set; }
        public string Type { get; set; }
        public DateTime Echeance { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? DateRealisation { get; set; }
        public int NombreDestinataires { get; set; }
        public int NombreEnvois { get; set; }
        public int NombreReponses { get; set; }
        public int EtatTraitement { get; set; }

        public virtual CampagneMail IdCampagneMailNavigation { get; set; }
        public virtual ICollection<DestinataireEnquete> DestinataireEnquete { get; set; }
    }
    public class CampagneMailModel
    {
        public CampagneMailModel()
        {
            PlanificationCampagneMailModel = new HashSet<PlanificationCampagneMailModel>();
        }

        public int IdCampagneMail { get; set; }
        public int IdQuestionnaire { get; set; }
        public DateTime DateCreation { get; set; }
        public string Description { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }

        public virtual OffreFormationModel Id { get; set; }
        public virtual ICollection<PlanificationCampagneMailModel> PlanificationCampagneMailModel { get; set; }
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
        public OffreFormationModel()
        {
            CampagneMailModel = new HashSet<CampagneMailModel>();
            BeneficiaireOffreFormation = new HashSet<BeneficiaireOffreFormation>();
        }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public int CodeProduitFormation { get; set; }
        public string LibelleOffreFormation { get; set; }
        public string LibelleReduitOffreFormation { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DateFinOffreFormation { get; set; }
        public virtual ICollection<BeneficiaireOffreFormation> BeneficiaireOffreFormation { get; set; }
        public virtual ICollection<CampagneMailModel> CampagneMailModel { get; set; }
    }
    public class BeneficiaireModel
    {
        public string MatriculeBeneficiaire { get; set; }
        public string TitreCivilite { get; set; }
        public string NomBeneficiaire { get; set; }
        public string PrenomBeneficiaire { get; set; }
        public string MailBeneficiaire { get; set; }

    }
    public class GestionEnqueteModel
    {
        public GestionEnqueteModel()
        {
            OffreFormationModels = new HashSet<OffreFormationModel>();
            BeneficiaireModels = new HashSet<BeneficiaireModel>();
            CampagneMailModel = new CampagneMailModel();
            OffreFormationModel = new OffreFormationModel();
            QuestionnaireModels = new HashSet<QuestionnaireModel>();
        }
        public virtual ICollection<QuestionnaireModel> QuestionnaireModels { get; set; }
        public CampagneMailModel CampagneMailModel { get; set; }
        public OffreFormationModel OffreFormationModel { get; set; }
        public virtual ICollection<OffreFormationModel> OffreFormationModels { get; set; }
        public virtual ICollection<BeneficiaireModel> BeneficiaireModels { get; set; }

    }
    public class DestinataireEnqueteModel
    {
        public Guid IdSoumissionnaire { get; set; }
        public int IdPlanificationCampagneMail { get; set; }
        public string MatriculeBeneficiaire { get; set; }
        public int IdOffreFormation { get; set; }
        public string IdEtablissement { get; set; }
        public DateTime DateEcheanceEnquete { get; set; }
        public DateTime? DateRealisationEnquete { get; set; }
        public int EtatTraitementQuestionnaire { get; set; }
        public bool? Agrege { get; set; }
        public int? EtapeQuestionnaire { get; set; }
        public DateTime? DateRelance1 { get; set; }
        public DateTime? DateRelance2 { get; set; }
        public int? IdContrat { get; set; }
        public bool? EnEmploi { get; set; }

        //public virtual Contrat IdContratNavigation { get; set; }
        //public virtual PlanificationCampagneMail IdPlanificationCampagneMailNavigation { get; set; }
        //public virtual Beneficiaire MatriculeBeneficiaireNavigation { get; set; }
    }
}
