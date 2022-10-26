CREATE TABLE [finance].[Payees]
(
	[PayeeId] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [RowVersion] ROWVERSION NOT NULL, 
    CONSTRAINT [PK_Payees] PRIMARY KEY ([PayeeId])
)
