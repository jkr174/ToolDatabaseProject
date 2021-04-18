CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CustomerID] INT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleName] CHAR NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NCHAR(64) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(15) NOT NULL
)
