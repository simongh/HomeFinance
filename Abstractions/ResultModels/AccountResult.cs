namespace HomeFinance.ResultModels
{
	public struct AccountResult : Mapping.IMapFrom<Entities.Account>
	{
		public int Id { get; init; }

		public string Name { get; init; }

		public string? Description { get; init; }

		public decimal Balance { get; init; }

		public decimal OpeningBalance { get; init; }
	}
}