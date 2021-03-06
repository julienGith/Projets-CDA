USE [OPS]
GO

INSERT [dbo].[ProduitFormation] ([CodeProduitFormation], [NiveauFormation], [LibelleProduitFormation], [LibelleCourtFormation], [FormationContinue], [FormationDiplomante]) VALUES (9953, N'II   ', N'Concepteur Développeur d''Applications', N'CDA', 0, 1)

INSERT INTO [dbo].[ProduitFormation_AppellationRome]
           ([CodeProduitFormation]
           ,[CodeRome])
     VALUES
           (9953
           ,'M1805')

INSERT [dbo].[OffreFormation] ([IdOffreFormation], [IdEtablissement], [MatriculeCollaborateurAfpa], [CodeProduitFormation], [LibelleOffreFormation], [LibelleReduitOffreFormation], [DateDebutOffreFormation], [DateFinOffreFormation]) VALUES (13097, N'19011', N'96GB011', 9953, N'Concept. Dév. Applications', N'CDA', CAST(N'2019-07-01' AS Date), CAST(N'2020-06-19' AS Date))

INSERT [dbo].[Beneficiaire] ([MatriculeBeneficiaire], [CodeTitreCivilite], [NomBeneficiaire], [PrenomBeneficiaire], [DateNaissanceBeneficiaire], [MailBeneficiaire], [TelBeneficiaire], [Ligne1Adresse], [Ligne2Adresse], [Ligne3Adresse], [CodePostal], [Ville], [UserID], [IdPays2], [PathPhoto]) VALUES (N'19005005', 0, N'LE THOMAS', N'Yann', NULL, N'exerebro@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Beneficiaire] ([MatriculeBeneficiaire], [CodeTitreCivilite], [NomBeneficiaire], [PrenomBeneficiaire], [DateNaissanceBeneficiaire], [MailBeneficiaire], [TelBeneficiaire], [Ligne1Adresse], [Ligne2Adresse], [Ligne3Adresse], [CodePostal], [Ville], [UserID], [IdPays2], [PathPhoto]) VALUES (N'19044235', 0, N'FERNANDEZ', N'Martin', NULL, N'ivor@laposte.net
', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Beneficiaire] ([MatriculeBeneficiaire], [CodeTitreCivilite], [NomBeneficiaire], [PrenomBeneficiaire], [DateNaissanceBeneficiaire], [MailBeneficiaire], [TelBeneficiaire], [Ligne1Adresse], [Ligne2Adresse], [Ligne3Adresse], [CodePostal], [Ville], [UserID], [IdPays2], [PathPhoto]) VALUES (N'19070578', 0, N'ESTACIO ABREU 
', N'Bernardo', NULL, N'bestacio89@gmail.com 
', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Beneficiaire] ([MatriculeBeneficiaire], [CodeTitreCivilite], [NomBeneficiaire], [PrenomBeneficiaire], [DateNaissanceBeneficiaire], [MailBeneficiaire], [TelBeneficiaire], [Ligne1Adresse], [Ligne2Adresse], [Ligne3Adresse], [CodePostal], [Ville], [UserID], [IdPays2], [PathPhoto]) VALUES (N'19071529', 0, N'BOISSERIE', N'Julien', NULL, N'jul.boisserie@gmail.com', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)


INSERT [dbo].[Beneficiaire_OffreFormation] ([MatriculeBeneficiaire], [IdOffreFormation], [IDEtablissement], [DateEntreeBeneficiaire], [DateSortieBeneficiaire], [Certifie]) VALUES (N'19005005', 13097, N'19011', CAST(N'2019-07-01' AS Date), CAST(N'2020-06-19' AS Date), N'ANT')
INSERT [dbo].[Beneficiaire_OffreFormation] ([MatriculeBeneficiaire], [IdOffreFormation], [IDEtablissement], [DateEntreeBeneficiaire], [DateSortieBeneficiaire], [Certifie]) VALUES (N'19044235', 13097, N'19011', CAST(N'2019-07-01' AS Date), CAST(N'2020-06-19' AS Date), N'ANT')
INSERT [dbo].[Beneficiaire_OffreFormation] ([MatriculeBeneficiaire], [IdOffreFormation], [IDEtablissement], [DateEntreeBeneficiaire], [DateSortieBeneficiaire], [Certifie]) VALUES (N'19070578', 13097, N'19011', CAST(N'2019-07-01' AS Date), CAST(N'2020-06-19' AS Date), N'ANT')
INSERT [dbo].[Beneficiaire_OffreFormation] ([MatriculeBeneficiaire], [IdOffreFormation], [IDEtablissement], [DateEntreeBeneficiaire], [DateSortieBeneficiaire], [Certifie]) VALUES (N'19071529', 13097, N'19011', CAST(N'2019-07-01' AS Date), CAST(N'2020-06-19' AS Date), N'ANT')
