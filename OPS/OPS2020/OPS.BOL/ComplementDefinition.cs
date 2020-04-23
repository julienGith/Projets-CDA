using System;
using System.Collections.Generic;
using System.Text;

namespace OPS.BOL
{
    public partial class CampagneMail
    {

        public override int GetHashCode()
        {
            return IdCampagneMail.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj as CampagneMail != null && obj.GetHashCode() == GetHashCode());
        }

        
    }
    public partial class Beneficiaire
    {
        public override bool Equals(object obj)
        {
            Beneficiaire beneficiaire = obj as Beneficiaire;
            return (beneficiaire == null ? false : beneficiaire.MatriculeBeneficiaire == this.MatriculeBeneficiaire);
        }
        public override int GetHashCode()
        {
            return (this.MatriculeBeneficiaire == null ? 0 : this.MatriculeBeneficiaire.GetHashCode());
        }
    }
    public partial class CollaborateurAfpa
    {
        public override bool Equals(object obj)
        {
            CollaborateurAfpa collaborateur = obj as CollaborateurAfpa;
            return (collaborateur == null ? false : collaborateur.MatriculeCollaborateurAfpa == this.MatriculeCollaborateurAfpa);
        }
        public override int GetHashCode()
        {
            return (this.MatriculeCollaborateurAfpa == null ? 0 : this.MatriculeCollaborateurAfpa.GetHashCode());
        }
    }
    public partial class Entreprise
    {
        public override bool Equals(object obj)
        {
            Entreprise entreprise = obj as Entreprise;
            return (entreprise == null ? false : entreprise.IdEntreprise == this.IdEntreprise);
        }
        public override int GetHashCode()
        {
            return (this.IdEntreprise == 0 ? 0 : this.IdEntreprise.GetHashCode());
        }
    }
    public partial class Etablissement
    {
        public override bool Equals(object obj)
        {
            Etablissement etablissement = obj as Etablissement;
            return (etablissement == null ? false : etablissement.IdEtablissement == this.IdEtablissement);
        }
        public override int GetHashCode()
        {
            return (string.IsNullOrEmpty(this.IdEtablissement) ? 0 : this.IdEtablissement.GetHashCode());
        }
    }
    public partial class OffreFormation
    {
        public override bool Equals(object obj)
        {
            OffreFormation offre = obj as OffreFormation;
            return (offre == null ? false : offre.IdOffreFormation == this.IdOffreFormation);
        }
        public override int GetHashCode()
        {
            return (this.IdOffreFormation == 0 ? 0 : this.IdOffreFormation.GetHashCode());
        }
    }
    public partial class PlanificationCampagneMail
    {
        public override int GetHashCode()
        {
            return IdPlanificationCampagneMail.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return (obj as PlanificationCampagneMail != null && obj.GetHashCode() == GetHashCode());
        }
    }
    public partial class ProduitFormation 
    {
        public override bool Equals(object obj)
        {
            ProduitFormation produit = obj as ProduitFormation;
            return (produit == null ? false : produit.CodeProduitFormation == this.CodeProduitFormation);
        }
        public override int GetHashCode()
        {
            return (this.CodeProduitFormation == 0 ? 0 : this.CodeProduitFormation.GetHashCode());
        }
    }
    }
