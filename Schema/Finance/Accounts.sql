CREATE TABLE [finance].[Accounts]
(
	[AccountId] INT NOT NULL IDENTITY , 
    [Name] NVARCHAR(100) NOT NULL, 
    [OpeningBalance] MONEY NOT NULL CONSTRAINT DF_Accounts_OpeningBalance DEFAULT 0, 
    [Balance] MONEY NOT NULL, 
    [RowVersion] ROWVERSION NOT NULL, 
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId])
)
