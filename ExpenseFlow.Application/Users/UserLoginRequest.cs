using System.ComponentModel.DataAnnotations;

namespace ExpenseFlow.Application.Users;

public class UserLoginRequest
{
    [Required(ErrorMessage = "UserName is required.")]
    public string? UserName { get; init; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}

