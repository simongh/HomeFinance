using AutoMapper;
using System;

namespace HomeFinance.ResultModels
{
	public struct TransactionSummaryResult : Mapping.IMapFrom<Entities.Transaction>
	{
		public int Id { get; init; }

		public DateOnly Created { get; init; }

		public string? Category { get; init; }

		public string? Payee { get; init; }

		public bool IsTransfer { get; init; }

		public decimal Value { get; init; }

		public TransactionStatus Status { get; init; }

		public TransactionType Type { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Entities.Transaction, TransactionSummaryResult>()
				.ForMember(m => m.Category, config => config.MapFrom(e => e.Category == null ? null : e.Category.Name))
				.ForMember(m => m.Payee, config => config.MapFrom(e => e.Payee == null ? null : e.Payee.Name))
				.ForMember(m => m.IsTransfer, config => config.MapFrom(e => e.LinkedTransaction != null));
		}
	}
}