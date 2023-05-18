CREATE TABLE Animal
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(200) NOT NULL, 
    [Description] NVARCHAR(200) NULL, 
    [Category] NVARCHAR(200) NOT NULL, 
    [Area] NVARCHAR(200) NOT NULL
)

INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (1, N'Labrador', N'Futrzasty', N'Ssak', N'Europa')
INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (2, N'Krowa', N'Miejska', N'Ssak', N'Europa')
INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (3, N'ŻółwGrecki', N'Pierdzący', N'Gady', N'Grecja')
INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (4, N'Ważka', N'Nadwodna', N'Owad', N'Wszędzie')
INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (5, N'Gołąb', N'Miejski', N'Ptak', N'Wszędzie')
INSERT INTO Animal ([ID], [Name], [Description], [Category], [Area]) VALUES (6, N'Ropucha', N'Górska', N'Płaz', N'Góry')
