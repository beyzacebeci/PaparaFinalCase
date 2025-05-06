namespace ExpenseFlow.DataAccess.PaymentTransactions;

public class PaymentTransaction : BaseEntity<int>
{
    public int ExpenseClaimId { get; set; }
    public string UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentReference { get; set; } = default!;
    public string PaymentStatus { get; set; } // success, fail
}

