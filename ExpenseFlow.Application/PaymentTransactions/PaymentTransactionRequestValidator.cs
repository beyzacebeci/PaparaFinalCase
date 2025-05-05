using FluentValidation;

namespace ExpenseFlow.Application.PaymentTransactions
{
    public class PaymentTransactionRequestValidator : AbstractValidator<PaymentTransactionRequest>
    {
        public PaymentTransactionRequestValidator()
        {
            RuleFor(x => x.ExpenseClaimId)
                .NotEmpty().WithMessage("ExpenseClaim ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.PaymentDate)
                .NotEmpty().WithMessage("Payment date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Payment date cannot be in the future.");

            RuleFor(x => x.PaymentReference)
                .NotEmpty().WithMessage("Payment reference is required.")
                .MaximumLength(100).WithMessage("Payment reference cannot exceed 100 characters.");

            RuleFor(x => x.PaymentStatus)
                .NotEmpty().WithMessage("Payment status is required.")
                .Must(status => status == "success" || status == "fail")
                .WithMessage("Payment status must be either 'success' or 'fail'.");
        }
    }
}