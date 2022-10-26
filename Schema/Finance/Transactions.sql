CREATE TABLE [finance].[Transactions]
(
	[TransactionId] INT NOT NULL IDENTITY, 
    [AccountId] INT NOT NULL, 
    [CategoryId] INT NULL, 
    [PayeeId] INT NULL, 
    [Created] DATE NOT NULL, 
    [Value] MONEY NOT NULL,
    [LinkedTransactionId] INT NULL, 
    [Memo] NVARCHAR(MAX) NULL, 
    [Status] TINYINT NOT NULL CONSTRAINT  DF_Transactions_Status DEFAULT 0, 
    [Type] TINYINT NULL, 
    [RowVersion] ROWVERSION NOT NULL, 
    CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY (AccountId) REFERENCES finance.Accounts(AccountId), 
    CONSTRAINT [FK_Transactions_Categories] FOREIGN KEY (CategoryId) REFERENCES finance.Categories(CategoryId), 
    CONSTRAINT [FK_Transactions_Payees] FOREIGN KEY (PayeeId) REFERENCES finance.Payees(PayeeId), 
    CONSTRAINT [PK_Transactions] PRIMARY KEY (TransactionId) 
)
