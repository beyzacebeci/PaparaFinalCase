using FluentValidation;

namespace ExpenseFlow.Application.ExpenseCategories
{
    public class ExpenseCategoryRequestValidator : AbstractValidator<ExpenseCategoryRequest>
    {
        public ExpenseCategoryRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name cannot be empty.")
                .MaximumLength(100).WithMessage("Category name must not exceed 150 characters.");
        }
    }
}