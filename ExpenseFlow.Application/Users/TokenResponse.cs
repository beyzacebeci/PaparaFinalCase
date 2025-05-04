namespace ExpenseFlow.Application.Users;

public class TokenResponse
{
    public String AccessToken { get; init; }
    public String RefreshToken { get; init; }
    public string UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string UserName { get; init; }
    public List<string> Roles { get; set; }  // Kullanıcı rollerini tutacak alan
}

