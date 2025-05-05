using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.Users;

namespace ExpenseFlow.DataAccess.ExpenseClaims;
public class ExpenseClaim : BaseEntity<int>
{
    public int ExpenseCategoryId { get; set; }
    public string? UserId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = default!;
    public string Location { get; set; } = default!;
    public DateTime ExpenseDate { get; set; }
    public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
    public string? ExpenseStatusDescription { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? PaymentReference { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public virtual ExpenseCategory ExpenseCategory { get; set; }
    public virtual User User { get; set; }
}

public enum ExpenseStatus
{
    Pending,
    Approved,
    Rejected,
    Paid
}

public enum PaymentMethod
{
    CreditCard,
    Cash,
    DebitCard,
    Other
}