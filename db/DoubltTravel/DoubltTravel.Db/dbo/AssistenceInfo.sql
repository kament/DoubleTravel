﻿CREATE TABLE [dbo].[AssistenceInfo]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Email] NVARCHAR(255) NOT NULL, 
    [Fax] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(50) NOT NULL, 
    [Globe] NVARCHAR(255) NOT NULL, 
    [Title] NVARCHAR(255) NOT NULL
)