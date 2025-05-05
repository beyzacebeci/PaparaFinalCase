using FluentValidation;
using ExpenseFlow.Application.ExpenseClaims;
using System;

namespace ExpenseFlow.Application.ExpenseClaims
{
    public class ExpenseClaimRequestValidator : AbstractValidator<ExpenseClaimRequest>
    {
        public ExpenseClaimRequestValidator()
        {
            RuleFor(x => x.ExpenseCategoryId)
                .GreaterThan(0).WithMessage("Expense category is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(200).WithMessage("Location must not exceed 200 characters.");

            RuleFor(x => x.ExpenseDate)
                .NotEmpty().WithMessage("Expense date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Expense date cannot be in the future.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum().WithMessage("Invalid payment method.");

        }
    }
}