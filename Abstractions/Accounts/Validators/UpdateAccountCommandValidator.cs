using FluentValidation;
using HomeFinance.Accounts.Commands;

namespace HomeFinance.Accounts.Validators
{
	internal class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
	{
		public UpdateAccountCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}