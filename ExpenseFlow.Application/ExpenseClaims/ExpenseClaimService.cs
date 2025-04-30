using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.Application.ExpenseClaims;

public class ExpenseClaimService : IExpenseClaimService
{
    private readonly IExpenseClaimRepository _expenseClaimRepository;

    public ExpenseClaimService(IExpenseClaimRepository expenseClaimRepository)
    {
        _expenseClaimRepository = expenseClaimRepository;
    }
}

