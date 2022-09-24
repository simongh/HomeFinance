﻿using AutoMapper;
using System;

namespace HomeFinance.ResultModels
{
	public struct TransactionResult : Mapping.IMapFrom<Entities.Transaction>
	{
		public int Id { get; init; }

		public DateOnly Created { get; init; }

		public TransactionStatus Status { get; init; }

		public TransactionType Type { get; init; }

		public int? PayeeId { get; init; }

		public int? CategoryId { get; init; }

		public int AccountId { get; init; }

		public decimal Value { get; init; }

		public bool IsTransfer { get; init; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Entities.Transaction, TransactionResult>()
				.ForMember(m => m.IsTransfer, config => config.MapFrom(e => e.LinkedTransactionId != null));
		}
	}
}