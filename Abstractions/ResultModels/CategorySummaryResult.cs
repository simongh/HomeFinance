namespace HomeFinance.ResultModels
{
	internal record struct CategorySummaryResult
	{
		public string Name { get; init; }

		public decimal Value { get; init; }
	}
}