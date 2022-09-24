using FluentValidation;

namespace HomeFinance.Validators
{
	internal class UpdateAccountCommandValidator : AbstractValidator<Commands.UpdateAccountCommand>
	{
		public UpdateAccountCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}