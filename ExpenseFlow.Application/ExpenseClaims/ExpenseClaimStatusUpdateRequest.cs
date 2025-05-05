using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.Application.ExpenseClaims;

public class ExpenseClaimStatusUpdateRequest
{
    public ExpenseStatus Status { get; set; }
    public string? ExpenseStatusDescription { get; set; } 

}

