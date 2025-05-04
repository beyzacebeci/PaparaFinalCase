using System.ComponentModel.DataAnnotations;

namespace ExpenseFlow.Application.Users;

public record UserRegistrationRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    [Required(ErrorMessage = "UserName is required")]
    public string? UserName { get; init; }
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
    public ICollection<string> Roles { get; init; } = new List<string>();
    public UserRegistrationRequest()
    {
        Password = "User123";
    }
}

