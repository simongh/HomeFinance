using FluentValidation;

namespace HomeFinance.Payees.Validators
{
	internal class UpdatePayeeCommandValidator : AbstractValidator<Commands.UpdatePayeeCommand>
	{
		public UpdatePayeeCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}