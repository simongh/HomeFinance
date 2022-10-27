namespace HomeFinance.Models
{
	public struct DashboardModel
	{
		public IEnumerable<ResultModels.AccountResult> Accounts { get; init; }

		public IEnumerable<ResultModels.CategorySummaryResult> Categories { get; init; }
	}
}