using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.Application.ExpenseClaims;

public record ExpenseClaimResponse
{
    public int Id { get; init; }
    public long ExpenseCategoryId { get; init; }
    public string CategoryName { get; init; } = default!; 
    public decimal Amount { get; init; }
    public string Location { get; init; } = default!;
    public DateTime ExpenseDate { get; init; }
    public string Description { get; init; } = default!;
    public ExpenseStatus Status { get; init; }
    public string? ExpenseStatusDescription { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public string? PaymentReference { get; init; }
    public DateTime? ApprovalDate { get; init; }

}

