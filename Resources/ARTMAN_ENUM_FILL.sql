USE [ARTMAN]
GO
SET IDENTITY_INSERT [dbo].[Autor] ON 

GO
INSERT [dbo].[Autor] ([ID], [Jmeno], [Prijmeni], [WebURL], [WikipediaURL], [Telefon], [Adresa], [Email], [Popis], [ResourcesDir]) VALUES (0, N'-', N'', N'', N'', N'', N'', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Autor] OFF
GO
SET IDENTITY_INSERT [dbo].[Majitel] ON 

GO
INSERT [dbo].[Majitel] ([Id], [Jmeno], [Prijmeni], [Telefon], [Adresa], [Email], [Poznamka]) VALUES (0, N'-', N'', N'', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Majitel] OFF
GO
SET IDENTITY_INSERT [dbo].[Technika] ON 

GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (0, N'-', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (1, N'Olejomalba', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (2, N'Kresba', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (3, N'Skica', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (4, N'Tisk', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (9, N'Bronz', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (10, N'Beton', N'')
GO
INSERT [dbo].[Technika] ([Id], [Nazev], [Popis]) VALUES (11, N'Ocel', N'')
GO
SET IDENTITY_INSERT [dbo].[Technika] OFF
GO
SET IDENTITY_INSERT [dbo].[TypDila] ON 

GO
INSERT [dbo].[TypDila] ([Id], [Nazev], [Popis]) VALUES (0, N'-', N'')
GO
INSERT [dbo].[TypDila] ([Id], [Nazev], [Popis]) VALUES (1, N'Obraz', N'Obraz')
GO
INSERT [dbo].[TypDila] ([Id], [Nazev], [Popis]) VALUES (2, N'Plastika', N'Plastika')
GO
INSERT [dbo].[TypDila] ([Id], [Nazev], [Popis]) VALUES (3, N'Rytina', N'')
GO
INSERT [dbo].[TypDila] ([Id], [Nazev], [Popis]) VALUES (4, N'Odlitek', N'')
GO
SET IDENTITY_INSERT [dbo].[TypDila] OFF
GO
SET IDENTITY_INSERT [dbo].[Umisteni] ON 

GO
INSERT [dbo].[Umisteni] ([Id], [Nazev], [Popis]) VALUES (0, N'-', N'')
GO
INSERT [dbo].[Umisteni] ([Id], [Nazev], [Popis]) VALUES (1, N'Trezor', N'')
GO
INSERT [dbo].[Umisteni] ([Id], [Nazev], [Popis]) VALUES (2, N'Vystava', N'')
GO
INSERT [dbo].[Umisteni] ([Id], [Nazev], [Popis]) VALUES (3, N'Galerie', N'')
GO
INSERT [dbo].[Umisteni] ([Id], [Nazev], [Popis]) VALUES (4, N'Doma', N'')
GO
SET IDENTITY_INSERT [dbo].[Umisteni] OFF
GO


SET IDENTITY_INSERT [dbo].[Mena] ON 
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (0, N'-', N'')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (1, N'EUR', N'Euro')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (2, N'JPY', N'Japanese yen')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (3, N'USD', N'US Dollar')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (4, N'CZK', N'Czech koruna')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (5, N'DKK', N'Danish krone')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (6, N'GBP', N'Pound sterling')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (7, N'HUF', N'Hungarian forint')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (8, N'PLN', N'Polish zloty')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (9, N'RON', N'Romanian leu')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (10, N'SEK', N'Swedish krona')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (11, N'CHF', N'Swiss franc')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (12, N'NOK', N'Norwegian krone')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (13, N'HRK', N'Croatian kuna')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (14, N'RUB', N'Russian rouble')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (15, N'TRY', N'Turkish lira')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (16, N'AUD', N'Australian dolla')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (17, N'BRL', N'Brazilian real')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (18, N'CAD', N'Canadian dollar')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (19, N'CNY', N'Chinese yuan ren')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (20, N'HKD', N'Hong Kong dollar')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (21, N'IDR', N'Indonesian rupia')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (22, N'ILS', N'Israeli shekel')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (23, N'INR', N'Indian rupee')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (24, N'KRW', N'South Korean won')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (25, N'MXN', N'Mexican peso')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (26, N'MYR', N'Malaysian ringgi')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (27, N'NZD', N'New Zealand dollar')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (28, N'PHP', N'Philippine peso')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (29, N'SGD', N'Singapore dollar')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (30, N'THB', N'Thai baht')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (31, N'ZAR', N'South African rand')
GO
INSERT [dbo].[Mena] ([Id], [Nazev], [Popis]) VALUES (32, N'ISK', N'Icelandic krona')
GO
SET IDENTITY_INSERT [dbo].[Mena] OFF
GO
SET IDENTITY_INSERT [dbo].[ProdejniMisto] ON 

GO
INSERT [dbo].[ProdejniMisto] ([Id], [Nazev], [Popis], [Telefon], [Adresa], [Email], [WebURL], [Poznamka]) VALUES (0, N'-', N'', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[ProdejniMisto] ([Id], [Nazev], [Popis], [Telefon], [Adresa], [Email], [WebURL], [Poznamka]) VALUES (1, N'Sotheby''s', N'', N'666', N'', N'', N'', N'')
GO
INSERT [dbo].[ProdejniMisto] ([Id], [Nazev], [Popis], [Telefon], [Adresa], [Email], [WebURL], [Poznamka]) VALUES (2, N'Christie''s', N'', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[ProdejniMisto] ([Id], [Nazev], [Popis], [Telefon], [Adresa], [Email], [WebURL], [Poznamka]) VALUES (3, N'Bonhams', N'', N'', N'', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[ProdejniMisto] OFF
GO

SET IDENTITY_INSERT [dbo].[KurzovniListek] ON 
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (1, 1, CAST(1.08080000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (2, 2, CAST(127.77000000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (3, 3, CAST(1.95580000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (4, 4, CAST(27.02600000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (5, 5, CAST(7.46210000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (6, 6, CAST(0.75459000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (7, 7, CAST(312.20000000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (8, 8, CAST(4.45870000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (9, 9, CAST(4.52730000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (10, 10, CAST(9.27380000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (11, 11, CAST(1.09500000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (12, 12, CAST(9.46850000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (13, 13, CAST(7.66900000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (14, 14, CAST(85.89300000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (15, 15, CAST(3.25720000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (16, 16, CAST(1.53800000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (17, 17, CAST(4.45500000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (18, 18, CAST(1.53160000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (19, 19, CAST(7.11040000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (20, 20, CAST(8.42040000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (21, 21, CAST(14952.60000000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (22, 22, CAST(4.29450000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (23, 23, CAST(73.09450000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (24, 24, CAST(1292.83000000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (25, 25, CAST(20.01600000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (26, 26, CAST(4.63080000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (27, 27, CAST(1.66060000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (28, 28, CAST(51.58000000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (29, 29, CAST(1.54210000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (30, 30, CAST(38.92100000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
INSERT [dbo].[KurzovniListek] ([Id], [MenaId], [EurRate], [Datum]) VALUES (31, 31, CAST(17.81430000 AS Decimal(28, 8)), CAST(N'1900-01-01' AS Date))
GO
SET IDENTITY_INSERT [dbo].[KurzovniListek] OFF
GO
