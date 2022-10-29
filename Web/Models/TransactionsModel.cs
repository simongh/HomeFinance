namespace HomeFinance.Models
{
	public struct TransactionsModel
	{
		public Transactions.Commands.GetTransactionsQuery Query { get; init; }

		public IEnumerable<Transactions.ResultModels.TransactionSummaryResult> Results { get; init; }

		public IEnumerable<Categories.ResultModels.CategoryResult> Categories { get; init; }

		public IEnumerable<Payees.ResultModels.PayeeResult> Payees { get; init; }

		public IEnumerable<TransactionType> Types => Enum.GetValues<TransactionType>();

		public IEnumerable<TransactionStatus> Statuses => Enum.GetValues<TransactionStatus>();
	}
}