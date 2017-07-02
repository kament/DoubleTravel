CREATE TABLE [dbo].[AssistenceInfo]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Email] NVARCHAR(255) NULL, 
    [Fax] NVARCHAR(50) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Globe] NVARCHAR(255) NULL, 
    [Title] NVARCHAR(255) NULL
)
