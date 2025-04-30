using System;

namespace ExpenseFlow.DataAccess.ExpenseClaims;

public class ExpenseClaimRepository : GenericRepository<ExpenseClaim,int>, IExpenseClaimRepository
{
    private readonly ExpenseFlowDbContext _context;

    public ExpenseClaimRepository(ExpenseFlowDbContext context) : base(context)
    {
        _context = context;
    }
}

 