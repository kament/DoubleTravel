﻿CREATE TABLE [dbo].[Representatives]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CountryId] INT NOT NULL, 
    [Address] NVARCHAR(255) NOT NULL, 
    [Email] NVARCHAR(255) NOT NULL, 
    [Fax] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL
)
