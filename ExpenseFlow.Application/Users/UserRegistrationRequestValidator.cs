using FluentValidation;

namespace ExpenseFlow.Application.Users
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Password)
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                 .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.Roles)
                .NotEmpty().WithMessage("At least one role must be assigned.");
        }
    }
}