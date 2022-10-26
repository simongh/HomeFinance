CREATE TABLE [finance].[Categories]
(
	[CategoryId] INT NOT NULL IDENTITY , 
    [Name] NVARCHAR(100) NOT NULL, 
    [ParentId] INT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [RowVersion] ROWVERSION NOT NULL, 
    CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId]), 
    CONSTRAINT [FK_Categories_Categories] FOREIGN KEY (ParentId) REFERENCES finance.Categories(CategoryId)
)
