using FluentValidation;
using HomeFinance.Categories.Commands;

namespace HomeFinance.Categories.Validators
{
	internal class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
	{
		public UpdateCategoryCommandValidator()
		{
			RuleFor(m => m.Name)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}