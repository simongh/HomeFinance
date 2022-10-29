using AutoMapper;
using System.Collections.Generic;

namespace HomeFinance.Categories.ResultModels
{
	public struct CategoryResult : Mapping.IMapFrom<Entities.Category>
	{
		public int Id { get; init; }

		public string Name { get; init; }

		public string Description { get; init; }

		public IEnumerable<CategoryResult>? Categories { get; init; }

		public int Transactions { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Entities.Category, CategoryResult>()
				.ForMember(p => p.Transactions, config => config.MapFrom(e => e.Transactions.Count));
		}
	}
}