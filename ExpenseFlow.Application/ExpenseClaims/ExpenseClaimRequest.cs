using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.Application.ExpenseClaims;

public record ExpenseClaimRequest
{
    public long ExpenseCategoryId { get; init; }
    public decimal Amount { get; init; }
    public string Location { get; init; } = default!;
    public DateTime ExpenseDate { get; init; }
    public string Description { get; init; } = default!;
    //public ExpenseStatus Status { get; init; }
    //public string? RejectionReason { get; init; }
    //public string? DocumentUrl { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    //public string? PaymentReference { get; init; }

    //public DateTime? ApprovalDate { get; init; }
    //public long? ApprovedByUserId { get; init; }
}


