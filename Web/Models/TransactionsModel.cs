namespace HomeFinance.Models
{
	public struct TransactionsModel
	{
		public Commands.GetTransactionsQuery Query { get; init; }

		public IEnumerable<ResultModels.TransactionSummaryResult> Results { get; init; }

		public IEnumerable<ResultModels.CategoryResult> Categories { get; init; }

		public IEnumerable<ResultModels.PayeeResult> Payees { get; init; }

		public IEnumerable<TransactionType> Types => Enum.GetValues<TransactionType>();

		public IEnumerable<TransactionStatus> Statuses => Enum.GetValues<TransactionStatus>();
	}
}