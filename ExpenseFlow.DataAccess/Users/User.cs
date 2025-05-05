using ExpenseFlow.DataAccess.ExpenseClaims;
using Microsoft.AspNetCore.Identity;

namespace ExpenseFlow.DataAccess.Users;
public class User : IdentityUser
{
    public String? FirstName { get; set; }
    public String? LastName { get; set; }
    public string? IBAN { get; set; }
    public decimal Balance { get; set; } // Kullanýcý bakiyesi
    public String? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
}
