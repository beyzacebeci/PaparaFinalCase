    using ExpenseFlow.DataAccess.ExpenseClaims;
    using Microsoft.AspNetCore.Identity;

    namespace ExpenseFlow.DataAccess.Users;
    public class User  : IdentityUser
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        //public string IBAN { get; set; }
        public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
    }
