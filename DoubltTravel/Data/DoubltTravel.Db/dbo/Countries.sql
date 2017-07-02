CREATE TABLE [dbo].[Countries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(90) NOT NULL, 
    [Code] NVARCHAR(5) NOT NULL UNIQUE, 
    [AssistenceInfoId] INT NOT NULL, 
    [CountryInfoId] INT NOT NULL
)
