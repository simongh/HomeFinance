using FluentValidation;

namespace HomeFinance.Validators
{
	internal class UpdateCategoryCommandValidator : AbstractValidator<Commands.UpdateCategoryCommand>
	{
		public UpdateCategoryCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}