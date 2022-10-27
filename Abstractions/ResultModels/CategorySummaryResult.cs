namespace HomeFinance.ResultModels
{
	public record struct CategorySummaryResult
	{
		public string? Name { get; init; }

		public decimal Value { get; init; }
	}
}