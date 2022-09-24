namespace HomeFinance
{
	internal class Constants
	{
	}

	public enum TransactionStatus : byte
	{
		None,
		Tracked,
		Reconciled,
		Deleted,
	}

	public enum TransactionType : byte
	{
		None,
		StandingOrder,
		DirectDebit,
		Cash,
		Deposit,
	}
}