using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OPS.BOL;

namespace OPS.DAL
{
    public partial class OPSCtx : DbContext
    {
        public OPSCtx()
        {
        }

        public OPSCtx(DbContextOptions<OPSCtx> options)
            : base(options)
        {
        }

        public virtual DbSet<AppelationRome> AppelationRome { get; set; }
        public virtual DbSet<Beneficiaire> Beneficiaire { get; set; }
        public virtual DbSet<BeneficiaireOffreFormation> BeneficiaireOffreFormation { get; set; }
        public virtual DbSet<CampagneMail> CampagneMail { get; set; }
        public virtual DbSet<ChampProfessionnnel> ChampProfessionnnel { get; set; }
        public virtual DbSet<CollaborateurAfpa> CollaborateurAfpa { get; set; }
        public virtual DbSet<Contrat> Contrat { get; set; }
        public virtual DbSet<DestinataireEnquete> DestinataireEnquete { get; set; }
        public virtual DbSet<Dictionnaire> Dictionnaire { get; set; }
        public virtual DbSet<DomaineMetierRome> DomaineMetierRome { get; set; }
        public virtual DbSet<Entreprise> Entreprise { get; set; }
        public virtual DbSet<Etablissement> Etablissement { get; set; }
        public virtual DbSet<FamilleMetierRome> FamilleMetierRome { get; set; }
        public virtual DbSet<OffreFormation> OffreFormation { get; set; }
        public virtual DbSet<Pays> Pays { get; set; }
        public virtual DbSet<PlanificationCampagneMail> PlanificationCampagneMail { get; set; }
        public virtual DbSet<ProduitFormation> ProduitFormation { get; set; }
        public virtual DbSet<ProduitFormationAppellationRome> ProduitFormationAppellationRome { get; set; }
        public virtual DbSet<Questionnaire> Questionnaire { get; set; }
        public virtual DbSet<ResultatCertification> ResultatCertification { get; set; }
        public virtual DbSet<Rome> Rome { get; set; }
        public virtual DbSet<TitreCivilite> TitreCivilite { get; set; }
        public virtual DbSet<TypeContrat> TypeContrat { get; set; }
        public virtual DbSet<TypeFonction> TypeFonction { get; set; }
        public virtual DbSet<UniteOrganisationnelle> UniteOrganisationnelle { get; set; }
        public virtual DbSet<UoChampProfessionnel> UoChampProfessionnel { get; set; }
        public virtual DbSet<WAgrOffreFormation> WAgrOffreFormation { get; set; }
        public virtual DbSet<WAgrQuestionnaire> WAgrQuestionnaire { get; set; }
        public virtual DbSet<WReponse> WReponse { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=localhost;Database=OPSDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppelationRome>(entity =>
            {
                entity.HasKey(e => e.CodeAppelationRome);

                entity.Property(e => e.CodeAppelationRome).ValueGeneratedNever();

                entity.Property(e => e.CodeRome)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LibelleAppelationRome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeRomeNavigation)
                    .WithMany(p => p.AppelationRome)
                    .HasForeignKey(d => d.CodeRome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppelationRome_CodeRome");
            });

            modelBuilder.Entity<Beneficiaire>(entity =>
            {
                entity.HasKey(e => e.MatriculeBeneficiaire);

                entity.Property(e => e.MatriculeBeneficiaire)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodePostal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateNaissanceBeneficiaire).HasColumnType("date");

                entity.Property(e => e.IdPays2)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ligne1Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne2Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne3Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MailBeneficiaire)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NomBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PathPhoto)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrenomBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TelBeneficiaire)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(128);

                entity.Property(e => e.Ville)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeTitreCiviliteNavigation)
                    .WithMany(p => p.Beneficiaire)
                    .HasForeignKey(d => d.CodeTitreCivilite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiaire_TitreCivilite");
            });

            modelBuilder.Entity<BeneficiaireOffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.MatriculeBeneficiaire, e.IdOffreFormation, e.Idetablissement });

                entity.ToTable("Beneficiaire_OffreFormation");

                entity.Property(e => e.MatriculeBeneficiaire)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Idetablissement)
                    .HasColumnName("IDEtablissement")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Certifie)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('ANT')");

                entity.Property(e => e.DateEntreeBeneficiaire).HasColumnType("date");

                entity.Property(e => e.DateSortieBeneficiaire).HasColumnType("date");

                entity.HasOne(d => d.CertifieNavigation)
                    .WithMany(p => p.BeneficiaireOffreFormation)
                    .HasForeignKey(d => d.Certifie)
                    .HasConstraintName("FK_Beneficiaire_OffreFormation_ResultatCertification");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.BeneficiaireOffreFormation)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiaire_OffreFormation_Beneficiaire");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.BeneficiaireOffreFormation)
                    .HasForeignKey(d => new { d.IdOffreFormation, d.Idetablissement })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Beneficiaire_OffreFormation_OffreFormation");
            });

            modelBuilder.Entity<CampagneMail>(entity =>
            {
                entity.HasKey(e => e.IdCampagneMail);

                entity.Property(e => e.DateCreation).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.IdEtablissement)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.CampagneMail)
                    .HasForeignKey(d => new { d.IdOffreFormation, d.IdEtablissement })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampagneMail_OffreFormation");
            });

            modelBuilder.Entity<ChampProfessionnnel>(entity =>
            {
                entity.HasKey(e => e.IdChampProfessionnel);

                entity.Property(e => e.IdChampProfessionnel)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LibelleChampProfessionnel)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CollaborateurAfpa>(entity =>
            {
                entity.HasKey(e => e.MatriculeCollaborateurAfpa)
                    .HasName("PK_CollaborateurAfpa_1");

                entity.Property(e => e.MatriculeCollaborateurAfpa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MailCollaborateurAfpa)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NomCollaborateur)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PrenomCollaborateur)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TelCollaborateurAfpa)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Uo)
                    .HasColumnName("UO")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(128);

                entity.HasOne(d => d.CodeTitreCiviliteNavigation)
                    .WithMany(p => p.CollaborateurAfpa)
                    .HasForeignKey(d => d.CodeTitreCivilite)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CollaborateurAfpa_TitreCivilite");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.CollaborateurAfpa)
                    .HasForeignKey(d => d.IdEtablissement)
                    .HasConstraintName("FK_CollaborateurAfpa_Etablissement");

                entity.HasOne(d => d.IdFonctionNavigation)
                    .WithMany(p => p.CollaborateurAfpa)
                    .HasForeignKey(d => d.IdFonction)
                    .HasConstraintName("FK_CollaborateurAfpa_TypeFonction");

                entity.HasOne(d => d.UoNavigation)
                    .WithMany(p => p.CollaborateurAfpa)
                    .HasForeignKey(d => d.Uo)
                    .HasConstraintName("FK_CollaborateurAfpa_UniteOrganisationnelle");
            });

            modelBuilder.Entity<Contrat>(entity =>
            {
                entity.HasKey(e => e.IdContrat);

                entity.Property(e => e.DateEntreeFonction).HasColumnType("date");

                entity.Property(e => e.DateSortieFonction).HasColumnType("date");

                entity.Property(e => e.LibelleFonction)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.MatriculeBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdEntrepriseNavigation)
                    .WithMany(p => p.Contrat)
                    .HasForeignKey(d => d.IdEntreprise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrat_Entreprise");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.Contrat)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrat_Beneficiaire");

                entity.HasOne(d => d.TypeContratNavigation)
                    .WithMany(p => p.Contrat)
                    .HasForeignKey(d => d.TypeContrat)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contrat_TypeContrat");
            });

            modelBuilder.Entity<DestinataireEnquete>(entity =>
            {
                entity.HasKey(e => e.IdSoumissionnaire);

                entity.Property(e => e.IdSoumissionnaire).ValueGeneratedNever();

                entity.Property(e => e.Agrege).HasDefaultValueSql("('0')");

                entity.Property(e => e.DateEcheanceEnquete).HasColumnType("date");

                entity.Property(e => e.DateRealisationEnquete).HasColumnType("datetime");

                entity.Property(e => e.DateRelance1).HasColumnType("date");

                entity.Property(e => e.DateRelance2).HasColumnType("date");

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MatriculeBeneficiaire)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdContratNavigation)
                    .WithMany(p => p.DestinataireEnquete)
                    .HasForeignKey(d => d.IdContrat)
                    .HasConstraintName("FK_DestinataireEnquete_Contrat");

                entity.HasOne(d => d.IdPlanificationCampagneMailNavigation)
                    .WithMany(p => p.DestinataireEnquete)
                    .HasForeignKey(d => d.IdPlanificationCampagneMail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestinataireEnquete_PlanificationCampagneMail");

                entity.HasOne(d => d.MatriculeBeneficiaireNavigation)
                    .WithMany(p => p.DestinataireEnquete)
                    .HasForeignKey(d => d.MatriculeBeneficiaire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DestinataireEnquete_Beneficiaire");
            });

            modelBuilder.Entity<Dictionnaire>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Dictionnaire");

                entity.Property(e => e.Colonne).HasMaxLength(128);

                entity.Property(e => e.Null)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Table)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Type).HasMaxLength(128);
            });

            modelBuilder.Entity<DomaineMetierRome>(entity =>
            {
                entity.HasKey(e => e.CodeDomaineRome);

                entity.Property(e => e.CodeDomaineRome)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodeFamilleRome)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IntituleDomaineRome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeFamilleRomeNavigation)
                    .WithMany(p => p.DomaineMetierRome)
                    .HasForeignKey(d => d.CodeFamilleRome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DomaineMetierRome_FamilleMetierRome");
            });

            modelBuilder.Entity<Entreprise>(entity =>
            {
                entity.HasKey(e => e.IdEntreprise);

                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Idpays2)
                    .IsRequired()
                    .HasColumnName("IDPays2")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ligne1Adresse)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne2Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne3Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MailEntreprise)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroSiret)
                    .IsRequired()
                    .HasColumnName("NumeroSIRET")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.RaisonSociale)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TelEntreprise)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Idpays2Navigation)
                    .WithMany(p => p.Entreprise)
                    .HasForeignKey(d => d.Idpays2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Entreprise_Pays");
            });

            modelBuilder.Entity<Etablissement>(entity =>
            {
                entity.HasKey(e => e.IdEtablissement)
                    .HasName("PK_Etablissement_1");

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodePostal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdEtablissementRattachement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Ligne1Adresse)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne2Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ligne3Adresse)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MailEtablissement)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.NomEtablissement)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TelEtablissement)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEtablissementRattachementNavigation)
                    .WithMany(p => p.InverseIdEtablissementRattachementNavigation)
                    .HasForeignKey(d => d.IdEtablissementRattachement)
                    .HasConstraintName("FK_Etablissement_Etablissement1");
            });

            modelBuilder.Entity<FamilleMetierRome>(entity =>
            {
                entity.HasKey(e => e.CodeFamilleMetierRome);

                entity.Property(e => e.CodeFamilleMetierRome)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IntituleFamilleMetierRome)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<OffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdOffreFormation, e.IdEtablissement });

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateDebutOffreFormation).HasColumnType("date");

                entity.Property(e => e.DateFinOffreFormation).HasColumnType("date");

                entity.Property(e => e.LibelleOffreFormation)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleReduitOffreFormation)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MatriculeCollaborateurAfpa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CodeProduitFormationNavigation)
                    .WithMany(p => p.OffreFormation)
                    .HasForeignKey(d => d.CodeProduitFormation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffreFormation_ProduitDeFormation");

                entity.HasOne(d => d.IdEtablissementNavigation)
                    .WithMany(p => p.OffreFormation)
                    .HasForeignKey(d => d.IdEtablissement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OffreFormation_Etablissement");

                entity.HasOne(d => d.MatriculeCollaborateurAfpaNavigation)
                    .WithMany(p => p.OffreFormation)
                    .HasForeignKey(d => d.MatriculeCollaborateurAfpa)
                    .HasConstraintName("FK_OffreFormation_CollaborateurAfpa");
            });

            modelBuilder.Entity<Pays>(entity =>
            {
                entity.HasKey(e => e.Idpays2);

                entity.Property(e => e.Idpays2)
                    .HasColumnName("IDPays2")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Idpays3)
                    .IsRequired()
                    .HasColumnName("IDPays3")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdpaysNum).HasColumnName("IDPaysNum");

                entity.Property(e => e.LibellePays)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PlanificationCampagneMail>(entity =>
            {
                entity.HasKey(e => e.IdPlanificationCampagneMail)
                    .HasName("PK_PlanificationCampagne");

                entity.Property(e => e.DateRealisation).HasColumnType("date");

                entity.Property(e => e.Echeance).HasColumnType("date");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdCampagneMailNavigation)
                    .WithMany(p => p.PlanificationCampagneMail)
                    .HasForeignKey(d => d.IdCampagneMail)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlanificationCampagneMail_CampagneMail");
            });

            modelBuilder.Entity<ProduitFormation>(entity =>
            {
                entity.HasKey(e => e.CodeProduitFormation)
                    .HasName("PK_ProduitDeFormation");

                entity.Property(e => e.CodeProduitFormation).ValueGeneratedNever();

                entity.Property(e => e.LibelleCourtFormation)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LibelleProduitFormation)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.NiveauFormation)
                    .HasMaxLength(5)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProduitFormationAppellationRome>(entity =>
            {
                entity.HasKey(e => new { e.CodeProduitFormation, e.CodeRome });

                entity.ToTable("ProduitFormation_AppellationRome");

                entity.Property(e => e.CodeRome)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CodeProduitFormationNavigation)
                    .WithMany(p => p.ProduitFormationAppellationRome)
                    .HasForeignKey(d => d.CodeProduitFormation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProduitFormation_AppellationRome_ProduitFormation");

                entity.HasOne(d => d.CodeRomeNavigation)
                    .WithMany(p => p.ProduitFormationAppellationRome)
                    .HasForeignKey(d => d.CodeRome)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProduitFormation_AppellationRome_Rome");
            });

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(e => e.IdQuestionnaire);

                entity.Property(e => e.DataJson)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TitreQuestionnaire)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeProduitFormationNavigation)
                    .WithMany(p => p.Questionnaire)
                    .HasForeignKey(d => d.CodeProduitFormation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questionnaire_ProduitFormation");
            });

            modelBuilder.Entity<ResultatCertification>(entity =>
            {
                entity.HasKey(e => e.CodeResultatCertification);

                entity.Property(e => e.CodeResultatCertification)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LibelleResultatCertification)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Rome>(entity =>
            {
                entity.HasKey(e => e.CodeRome)
                    .HasName("PK_CodeRome");

                entity.Property(e => e.CodeRome)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CodeDomaineRome)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IntituleCodeRome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodeDomaineRomeNavigation)
                    .WithMany(p => p.Rome)
                    .HasForeignKey(d => d.CodeDomaineRome)
                    .HasConstraintName("FK_Rome_DomaineMetierRome");
            });

            modelBuilder.Entity<TitreCivilite>(entity =>
            {
                entity.HasKey(e => e.CodeTitreCivilite);

                entity.Property(e => e.CodeTitreCivilite).ValueGeneratedNever();

                entity.Property(e => e.TitreCiviliteAbrege)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TitreCiviliteComplet)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeContrat>(entity =>
            {
                entity.HasKey(e => e.IdTypeContrat);

                entity.Property(e => e.DesignationTypeContrat)
                    .IsRequired()
                    .HasColumnName("designationTypeContrat")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeFonction>(entity =>
            {
                entity.HasKey(e => e.IdFonction);

                entity.Property(e => e.LibelleFonction)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UniteOrganisationnelle>(entity =>
            {
                entity.HasKey(e => e.Uo);

                entity.Property(e => e.Uo)
                    .HasColumnName("UO")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LibelleUo)
                    .IsRequired()
                    .HasColumnName("LibelleUO")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UoChampProfessionnel>(entity =>
            {
                entity.HasKey(e => new { e.Uo, e.IdChampProfessionnel });

                entity.ToTable("Uo_ChampProfessionnel");

                entity.Property(e => e.Uo)
                    .HasColumnName("UO")
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdChampProfessionnel)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdChampProfessionnelNavigation)
                    .WithMany(p => p.UoChampProfessionnel)
                    .HasForeignKey(d => d.IdChampProfessionnel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Uo_ChampProfessionnel_ChampProfessionnnel");

                entity.HasOne(d => d.UoNavigation)
                    .WithMany(p => p.UoChampProfessionnel)
                    .HasForeignKey(d => d.Uo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Uo_ChampProfessionnel_UniteOrganisationnelle");
            });

            modelBuilder.Entity<WAgrOffreFormation>(entity =>
            {
                entity.HasKey(e => new { e.IdPlanificationCampagneMail, e.NumOrdre })
                    .HasName("FK_W_AGR_OffreFormation");

                entity.ToTable("W_AGR_OffreFormation");

                entity.Property(e => e.NumOrdre)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Valeurs)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WAgrQuestionnaire>(entity =>
            {
                entity.HasKey(e => new { e.IdQuestionnaire, e.Type, e.NumOrdre });

                entity.ToTable("W_AGR_Questionnaire");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NumOrdre)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Valeurs)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WReponse>(entity =>
            {
                entity.HasKey(e => new { e.IdQuestion, e.IdQuestionnaire, e.IdSoumissionaire });

                entity.ToTable("W_Reponse");

                entity.Property(e => e.RawValue).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
