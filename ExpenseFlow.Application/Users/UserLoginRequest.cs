using System.ComponentModel.DataAnnotations;

namespace ExpenseFlow.Application.Users;

public class UserLoginRequest
{
    public string? UserName { get; init; }

    public string? Password { get; set; }
}

