﻿using FluentValidation;

namespace HomeFinance.Transactions.Validators
{
	internal class UpdateTransactionCommandValidator : AbstractValidator<Commands.UpdateTransactionCommand>
	{
		public UpdateTransactionCommandValidator()
		{
			RuleFor(m => m.Account)
				.NotEmpty();

			RuleFor(m => m.Type)
				.IsInEnum();

			RuleFor(m => m.Status)
				.IsInEnum();
		}
	}
}