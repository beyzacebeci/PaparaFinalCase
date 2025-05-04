using ExpenseFlow.DataAccess.ExpenseCategories;

namespace ExpenseFlow.DataAccess.ExpenseClaims;
public class ExpenseClaim : BaseEntity<int>
{
    //public long UserId { get; set; }
    public int CategoryId { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public string Location { get; set; } = default!;
    public DateTime ExpenseDate { get; set; }
    public string Description { get; set; } = default!;
    public ExpenseStatus Status { get; set; }
    public string? RejectionReason { get; set; }
    public string? DocumentUrl { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? PaymentReference { get; set; }
    public DateTime? ApprovalDate { get; set; }


    //public long? ApprovedByUserId { get; set; }

    //public virtual User User { get; set; }
    //public virtual User? ApprovedByUser { get; set; }
    public virtual ExpenseCategory Category { get; set; }
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