using AutoMapper;

namespace HomeFinance.ResultModels
{
	public struct PayeeResult : Mapping.IMapFrom<Entities.Payee>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? Description { get; set; }
		public int Transactions { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Entities.Payee, PayeeResult>()
				.ForMember(p => p.Transactions, config => config.MapFrom(e => e.Transactions.Count));
		}
	}
}