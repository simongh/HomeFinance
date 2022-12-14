using AutoMapper;
using System;

namespace HomeFinance.Transactions.ResultModels
{
	public struct TransactionResult : Mapping.IMapFrom<Entities.Transaction>
	{
		public int Id { get; init; }

		public DateOnly Created { get; init; }

		public TransactionStatus Status { get; init; }

		public TransactionType Type { get; init; }

		public int? Payee { get; init; }

		public int? Category { get; init; }

		public int Account { get; init; }

		public decimal Value { get; init; }

		public bool IsTransfer { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Entities.Transaction, TransactionResult>()
				.ForMember(m => m.IsTransfer, config => config.MapFrom(e => e.LinkedTransactionId != null))
				.ForMember(m => m.Payee, config => config.MapFrom(m => m.PayeeId))
				.ForMember(m => m.Category, config => config.MapFrom(m => m.CategoryId))
				.ForMember(m => m.Account, config => config.MapFrom(m => m.AccountId));
		}
	}
}