namespace HomeFinance.Models
{
	public struct TransactionsModel
	{
		public Commands.GetTransactionsQuery Query { get; init; }

		public IEnumerable<ResultModels.TransactionSummaryResult> Results { get; init; }

		public IEnumerable<ResultModels.CategoryResult> Categories { get; init; }

		public IEnumerable<ResultModels.PayeeResult> Payees { get; init; }
	}
}