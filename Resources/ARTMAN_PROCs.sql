USE [ARTMAN]
GO
/****** Object:  StoredProcedure [dbo].[spAutor_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spAutor_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Autor WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spAutor_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spAutor_Insert]	(
@Jmeno			NVARCHAR(max)
, @Prijmeni			NVARCHAR(max)	= ''
, @WebURL			NVARCHAR(max)	= ''
, @WikipediaURL		NVARCHAR(max)	= ''
, @Telefon			NVARCHAR(max)	= ''
, @Adresa			NVARCHAR(max)	= ''
, @Email			NVARCHAR(max)	= ''
, @Popis			NVARCHAR(max)	= ''
, @ResourcesDir		NVARCHAR(max)	= ''
)
AS
BEGIN

	INSERT INTO Autor (
	Jmeno			
	, Prijmeni		
	, WebURL		
	, WikipediaURL	
	, Telefon		
	, Adresa		
	, Email		
	, Popis		
	, ResourcesDir	
	)
	VALUES (
	@Jmeno			
	, @Prijmeni		
	, @WebURL		
	, @WikipediaURL	
	, @Telefon		
	, @Adresa		
	, @Email		
	, @Popis		
	, @ResourcesDir	
	)
	
	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spAutor_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spAutor_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM Autor WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spAutor_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spAutor_SelectList]
AS
BEGIN
SELECT * FROM Autor
END

GO
/****** Object:  StoredProcedure [dbo].[spAutor_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spAutor_Update]	(
@Id					INT
, @Jmeno			NVARCHAR(max)
, @Prijmeni			NVARCHAR(max)	= ''
, @WebURL			NVARCHAR(max)	= ''
, @WikipediaURL		NVARCHAR(max)	= ''
, @Telefon			NVARCHAR(max)	= ''
, @Adresa			NVARCHAR(max)	= ''
, @Email			NVARCHAR(max)	= ''
, @Popis			NVARCHAR(max)	= ''
, @ResourcesDir		NVARCHAR(max)	= ''
)
AS
BEGIN

	UPDATE Autor
	SET
	Jmeno				= @Jmeno			
	, Prijmeni			= @Prijmeni			
	, WebURL			= @WebURL			
	, WikipediaURL		= @WikipediaURL		
	, Telefon			= @Telefon			
	, Adresa			= @Adresa			
	, Email				= @Email			
	, Popis				= @Popis			
	, ResourcesDir		= @ResourcesDir		

	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spDilo_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spDilo_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Dilo WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spDilo_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spDilo_Insert] (
@Koupeno			DATE			= '19000101'
, @Prodano			DATE			= '19000101'
, @NakupCena		DECIMAL(28,8)	= 0
, @NakupMenaId		INT				= 0
, @ProdejCena		DECIMAL(28,8)	= 0
, @ProdejMenaId		INT				= 0
, @AutorId			INT				= 0
, @Nazev			NVARCHAR(max)	= ''
, @Rok				INT				= 0
, @TechnikaId		INT				= 0
, @Rozmer			NVARCHAR(max)	= ''
, @Skladem			BIT				= 0
, @Majitel1Id		INT				= 0
, @Majitel2Id		INT				= 0
, @KoupenoKdeId		INT				= 0
, @ProdanoKdeId		INT				= 0
, @TypDilaId		INT				= 0
, @UmisteniId		INT				= 0
, @WikipediaURL		NVARCHAR(max)	= ''
, @ResourcesDir		NVARCHAR(max)	= ''
, @JeProdano		BIT				= 0
)
AS
BEGIN

	INSERT INTO Dilo (
	Koupeno		
	, Prodano		
	, NakupCena	
	, NakupMenaId	
	, ProdejCena	
	, ProdejMenaId	
	, AutorId		
	, Nazev		
	, Rok			
	, TechnikaId	
	, Rozmer		
	, Skladem		
	, Majitel1Id	
	, Majitel2Id	
	, KoupenoKdeId	
	, ProdanoKdeId	
	, TypDilaId	
	, UmisteniId	
	, WikipediaURL
	, ResourcesDir
	, JeProdano
	)
	VALUES (
	@Koupeno		
	, @Prodano		
	, @NakupCena	
	, @NakupMenaId	
	, @ProdejCena	
	, @ProdejMenaId	
	, @AutorId		
	, @Nazev		
	, @Rok			
	, @TechnikaId	
	, @Rozmer		
	, @Skladem		
	, @Majitel1Id	
	, @Majitel2Id	
	, @KoupenoKdeId	
	, @ProdanoKdeId	
	, @TypDilaId	
	, @UmisteniId	
	, @WikipediaURL
	, @ResourcesDir
	, @JeProdano
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spDilo_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spDilo_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM Dilo WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spDilo_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spDilo_SelectList] ( 
@KoupenoOd		DATE	= '19000101'
, @KoupenoDo	DATE	= '19000101'
, @ProdanoOd	DATE	= '19000101'
, @ProdanoDo	DATE	= '19000101'
, @Skladem		INT		= -1 -- -1 vse 0=0 1=1
, @AutorId		INT		= 0
, @JeProdano	int		= -1 -- -1 vse 0=0 1=1
)
AS
BEGIN
	SELECT 
	Dilo.Id								[Id]					--[Id]
	, Dilo.Koupeno						[DiloKoupeno]			--[Koupeno kdy]
	, Dilo.NakupCena 					[NakupCena] 		--[Kupni cena]
	, KupniMena.Nazev 					[KupniMenaNazev] 		--[Kupni Mena]
	, KoupenoKde.Nazev					[KoupenoKdeNazev]		--[Koupeno v]
	, Dilo.JeProdano 					[Prodano] 			--[je Prodano ?]
	, Dilo.Prodano 						[DiloProdano] 			--[Prodano kdy]
	, ProdejCena 						[ProdejCena] 			--[Prodejni cena]
	, ProdejMena.Nazev					[ProdejMenaNazev]		--[Prodejni mena]
	, ProdanoKde.Nazev					[ProdanoKdeNazev]		--[Prodano v]
	, Autor.Jmeno						[AutorJmeno]			--[Jmeno autora]
	, Autor.Prijmeni					[AutorPrijmeni]			--[Prijmeni autora]
	, Dilo.Nazev						[DiloNazev]				--[Nazev dila]
	, Dilo.Rok 							[DiloRok] 				--[Rok vytvoreni]
	, TypDila.Nazev 					[TypDilaNazev] 			--[Typ dila]
	, Technika.Nazev					[TechnikaNazev]			--[Technika]
	, Dilo.Rozmer						[DiloRozmer]			--[Rozmer]
	, Dilo.Skladem						[DiloSkladem]			--[Skladem]
	, Majitel1.Jmeno					[Majitel1Jmeno]			--[Majitel #1 Jmeno]
	, Majitel1.Prijmeni					[Majitel1Prijmeni]		--[Majitel #1 Prijmeni]
	, Majitel2.Jmeno					[Majitel2Jmeno]			--[Majitel #2 Jmeno]
	, Majitel2.Prijmeni					[Majitel2Prijmeni]		--[Majitel #2 Prijmeni]
	, Umisteni.Nazev					[UmisteniNazev]			--[Umisteni]
	FROM  Dilo
	INNER JOIN Autor
		ON Autor.ID = Dilo.AutorId 
	INNER JOIN Majitel Majitel1 
		ON Dilo.Majitel1Id = Majitel1.Id
	INNER JOIN Majitel Majitel2 
		ON Dilo.Majitel2Id = Majitel2.Id
	INNER JOIN Mena KupniMena
		ON Dilo.NakupMenaId = KupniMena.Id 
	INNER JOIN Mena ProdejMena
		ON Dilo.NakupMenaId = ProdejMena.Id
	INNER JOIN ProdejniMisto KoupenoKde
		ON Dilo.KoupenoKdeId = KoupenoKde.Id
	INNER JOIN ProdejniMisto ProdanoKde
		ON Dilo.ProdanoKdeId = ProdanoKde.Id 
	INNER JOIN Technika		
		ON Dilo.TechnikaId = Technika.Id 
	INNER JOIN TypDila 
		ON Dilo.TypDilaId = TypDila.Id 
	INNER JOIN Umisteni 
		ON Dilo.UmisteniId = Umisteni.Id
	WHERE 
	(Dilo.Koupeno >= @KoupenoOd	OR @KoupenoOd = '19000101'	OR @KoupenoOd IS NULL)
	AND (Dilo.Koupeno >= @KoupenoOd	OR @KoupenoOd = '19000101'	OR @KoupenoOd IS NULL)
	AND (Dilo.Prodano >= @ProdanoOd	OR @ProdanoOd = '19000101'	OR @ProdanoOd IS NULL) 
	AND (Dilo.Prodano >= @ProdanoOd	OR @ProdanoOd = '19000101'	OR @ProdanoOd IS NULL) 
	AND (Dilo.Skladem = @Skladem OR @Skladem = -1)
	AND (Dilo.AutorId = @AutorId OR @AutorId = 0)
	AND (Dilo.JeProdano = @JeProdano OR @JeProdano = -1)
	
END

GO
/****** Object:  StoredProcedure [dbo].[spDilo_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spDilo_Update] (
@Id					INT 
, @Koupeno			DATE			= '19000101'
, @Prodano			DATE			= '19000101'
, @NakupCena		DECIMAL(28,8)	= 0
, @NakupMenaId		INT				= 0
, @ProdejCena		DECIMAL(28,8)	= 0
, @ProdejMenaId		INT				= 0
, @AutorId			INT				= 0
, @Nazev			NVARCHAR(max)	= ''
, @Rok				INT				= 0
, @TechnikaId		INT				= 0
, @Rozmer			NVARCHAR(max)	= ''
, @Skladem			BIT				= 0
, @Majitel1Id		INT				= 0
, @Majitel2Id		INT				= 0
, @KoupenoKdeId		INT				= 0
, @ProdanoKdeId		INT				= 0
, @TypDilaId		INT				= 0
, @UmisteniId		INT				= 0
, @WikipediaURL		NVARCHAR(max)	= ''
, @ResourcesDir		NVARCHAR(max)	= ''
, @JeProdano		BIT				= 0
)
AS
BEGIN

	UPDATE Dilo
	SET
	Koupeno			 = @Koupeno		
	, Prodano		 = @Prodano		
	, NakupCena		 = @NakupCena	
	, NakupMenaId	 = @NakupMenaId	
	, ProdejCena	 = @ProdejCena	
	, ProdejMenaId	 = @ProdejMenaId	
	, AutorId		 = @AutorId		
	, Nazev			 = @Nazev		
	, Rok			 = @Rok			
	, TechnikaId	 = @TechnikaId	
	, Rozmer		 = @Rozmer		
	, Skladem		 = @Skladem		
	, Majitel1Id	 = @Majitel1Id	
	, Majitel2Id	 = @Majitel2Id	
	, KoupenoKdeId	 = @KoupenoKdeId	
	, ProdanoKdeId	 = @ProdanoKdeId	
	, TypDilaId		 = @TypDilaId	
	, UmisteniId	 = @UmisteniId	
	, WikipediaURL  = @WikipediaURL
	, ResourcesDir  = @ResourcesDir	
	, JeProdano		= @JeProdano
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spKurzovniListek_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spKurzovniListek_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM KurzovniListek WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spKurzovniListek_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spKurzovniListek_Insert] (
@MenaId			INT
, @EurRate			DECIMAL(28,8)
, @Datum			DATE
) 
AS
BEGIN


	INSERT INTO KurzoKurzovniListek(
	MenaId		
	, EurRate	
	, Datum	
	)
	VALUES (
	@MenaId		
	, @EurRate	
	, @Datum	
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spKurzovniListek_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spKurzovniListek_SelectDetails]
 (
@Id					INT
)
AS
BEGIN
SELECT * FROM kurzovniListek WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spKurzovniListek_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spKurzovniListek_SelectList]
AS
BEGIN
SELECT * FROM KurzovniListek
END

GO
/****** Object:  StoredProcedure [dbo].[spKurzovniListek_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spKurzovniListek_Update] (
@Id					INT 
, @MenaId			INT
, @EurRate			DECIMAL(28,8)
, @Datum			DATE
) 
AS
BEGIN

	UPDATE KurzovniListek
	SET
	MenaId		= @MenaId	
	, EurRate	= @EurRate
	, Datum		= @Datum
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spMajitel_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spMajitel_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Majitel WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spMajitel_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMajitel_Insert] ( 
@Jmeno			NVARCHAR(max)
, @Prijmeni			NVARCHAR(max)	= ''
, @Telefon			NVARCHAR(max)	= ''
, @Adresa			NVARCHAR(max)	= ''
, @Email			NVARCHAR(max)	= ''
, @Poznamka			NVARCHAR(max)	= ''
) 
AS
BEGIN

	INSERT INTO Majitel (
	Jmeno		
	, Prijmeni	
	, Telefon	
	, Adresa	
	, Email	
	, Poznamka	
	)
	VALUES (
	@Jmeno		
	, @Prijmeni	
	, @Telefon	
	, @Adresa	
	, @Email	
	, @Poznamka	
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spMajitel_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spMajitel_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM Majitel WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spMajitel_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spMajitel_SelectList]
AS
BEGIN
SELECT * FROM Majitel
END

GO
/****** Object:  StoredProcedure [dbo].[spMajitel_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMajitel_Update] ( 
@Id					INT 
, @Jmeno			NVARCHAR(max)
, @Prijmeni			NVARCHAR(max)	= ''
, @Telefon			NVARCHAR(max)	= ''
, @Adresa			NVARCHAR(max)	= ''
, @Email			NVARCHAR(max)	= ''
, @Poznamka			NVARCHAR(max)	= ''
) 
AS
BEGIN

	
	UPDATE Majitel
	SET
	Jmeno			= @Jmeno	
	, Prijmeni		= @Prijmeni
	, Telefon		= @Telefon	
	, Adresa		= @Adresa	
	, Email			= @Email	
	, Poznamka		= @Poznamka
	WHERE Id = @Id
	
	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spMena_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMena_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Mena WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spMena_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMena_Insert] ( 
@Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	INSERT INTO Mena (
	Nazev
	, Popis
	)
	VALUES (
	@Nazev
	, @Popis
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spMena_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMena_SelectDetails]
 (
@Id					INT
)
AS
BEGIN
SELECT * FROM Mena WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spMena_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMena_SelectList]
AS
BEGIN
SELECT * FROM Mena
END

GO
/****** Object:  StoredProcedure [dbo].[spMena_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spMena_Update] ( 
@Id					INT 
, @Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	UPDATE Mena
	SET
	Nazev			= @Nazev	
	, Popis			= @Popis
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spOceneni_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spOceneni_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Oceneni WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spOceneni_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spOceneni_Insert] (
@Datum			DATE
, @Cena				DECIMAL(28,8)
, @MenaId			INT
, @DiloId			INT	
) 
AS
BEGIN

	INSERT INTO Oceneni (
	Datum		
	, Cena		
	, MenaId	
	, DiloId	
	)
	VALUES (
	@Datum		
	, @Cena		
	, @MenaId	
	, @DiloId	
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spOceneni_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spOceneni_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM Oceneni WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spOceneni_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spOceneni_SelectList]
AS
BEGIN
SELECT * FROM Oceneni
END

GO
/****** Object:  StoredProcedure [dbo].[spOceneni_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spOceneni_Update] (
@Id					INT
, @Datum			DATE
, @Cena				DECIMAL(28,8)
, @MenaId			INT
, @DiloId			INT	
) 
AS
BEGIN

	UPDATE Oceneni
	SET
	Datum			= @Datum	
	, Cena			= @Cena	
	, MenaId		= @MenaId
	, DiloId		= @DiloId

	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spProdejniMisto_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spProdejniMisto_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM ProdejniMisto WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spProdejniMisto_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spProdejniMisto_Insert] (
@Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)
, @Telefon			NVARCHAR(max)	= ''
, @Adresa			NVARCHAR(max)	= ''
, @Email			NVARCHAR(max)	= ''
, @WebURL			NVARCHAR(max)	= ''
, @Poznamka			NVARCHAR(max)	= ''
) 
AS
BEGIN

	INSERT INTO ProdejniMisto (
	Nazev	
	, Popis	
	, Telefon	
	, Adresa	
	, Email	
	, WebURL	
	, Poznamka	
	)
	VALUES (
	@Nazev	
	, @Popis	
	, @Telefon	
	, @Adresa	
	, @Email	
	, @WebURL	
	, @Poznamka	
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spProdejniMisto_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spProdejniMisto_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM ProdejniMisto WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spProdejniMisto_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spProdejniMisto_SelectList]
AS
BEGIN
SELECT * FROM ProdejniMisto
END

GO
/****** Object:  StoredProcedure [dbo].[spProdejniMisto_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spProdejniMisto_Update] (
@Id					INT 
, @Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)
, @Telefon			NVARCHAR(max)	= ''
, @Adresa			NVARCHAR(max)	= ''
, @Email			NVARCHAR(max)	= ''
, @WebURL			NVARCHAR(max)	= ''
, @Poznamka			NVARCHAR(max)	= ''
) 
AS
BEGIN

	UPDATE ProdejniMisto
	SET
	Nazev			= @Nazev	
	, Popis			= @Popis	
	, Telefon		= @Telefon	
	, Adresa		= @Adresa	
	, Email			= @Email	
	, WebURL		= @WebURL	
	, Poznamka		= @Poznamka
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spTechnika_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spTechnika_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Technika WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spTechnika_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spTechnika_Insert] (
@Nazev			NVARCHAR(max)
, @Popis		NVARCHAR(max)	= ''
) 
AS
BEGIN

	INSERT INTO Technika (
	Nazev	
	, Popis
	)
	VALUES (
	@Nazev	
	, @Popis
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spTechnika_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spTechnika_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM Technika WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spTechnika_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spTechnika_SelectList]
AS
BEGIN
SELECT * FROM Technika
END

GO
/****** Object:  StoredProcedure [dbo].[spTechnika_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spTechnika_Update] (
@Id					INT 
, @Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	UPDATE Technika
	SET
	Nazev			= @Nazev	
	, Popis			= @Popis
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spTypDila_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spTypDila_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM TypDila WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spTypDila_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spTypDila_Insert] (
  @Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	INSERT INTO TypDila (
	Nazev
	, Popis
	)
	VALUES (
	@Nazev
	, @Popis
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spTypDila_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spTypDila_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM TypDila WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spTypDila_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spTypDila_SelectList]
AS
BEGIN
SELECT * FROM TypDila
END

GO
/****** Object:  StoredProcedure [dbo].[spTypDila_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spTypDila_Update] (
@Id					INT 
, @Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	UPDATE TypDila
	SET
	Nazev			= @Nazev	
	, Popis			= @Popis
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spUmisteni_Delete]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[spUmisteni_Delete] (
@Id					INT
)
AS
BEGIN

	DELETE FROM Umisteni WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
/****** Object:  StoredProcedure [dbo].[spUmisteni_Insert]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spUmisteni_Insert] (
@Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	INSERT INTO Umisteni (
	Nazev
	, Popis
	)
	VALUES (
	@Nazev
	, @Popis
	)

	SELECT		SCOPE_IDENTITY() [Id]

END

GO
/****** Object:  StoredProcedure [dbo].[spUmisteni_SelectDetails]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spUmisteni_SelectDetails] (
@Id					INT
)
AS
BEGIN
SELECT * FROM Umisteni WHERE Id = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[spUmisteni_SelectList]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[spUmisteni_SelectList]
AS
BEGIN
SELECT * FROM Umisteni
END

GO
/****** Object:  StoredProcedure [dbo].[spUmisteni_Update]    Script Date: 12-Jul-16 09:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spUmisteni_Update] (
@Id					INT 
, @Nazev			NVARCHAR(max)
, @Popis			NVARCHAR(max)	= ''
) 
AS
BEGIN

	UPDATE Umisteni
	SET
	Nazev			= @Nazev	
	, Popis			= @Popis
	WHERE Id = @Id

	SELECT		@@ROWCOUNT	[RowCount]

END

GO
