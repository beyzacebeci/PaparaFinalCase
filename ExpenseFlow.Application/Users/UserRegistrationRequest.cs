using System.ComponentModel.DataAnnotations;

namespace ExpenseFlow.Application.Users;

public record UserRegistrationRequest
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? UserName { get; init; }
    public string? Password { get; init; }
    public ICollection<string> Roles { get; init; } = new List<string>();

}

