namespace HomeFinance.Models
{
	public struct DashboardModel
	{
		public IEnumerable<Accounts.ResultModels.AccountResult> Accounts { get; init; }

		public IEnumerable<Categories.ResultModels.CategorySummaryResult> Categories { get; init; }
	}
}