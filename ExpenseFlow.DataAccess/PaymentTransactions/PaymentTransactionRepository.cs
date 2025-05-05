using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.DataAccess.PaymentTransactions;

public class PaymentTransactionRepository : GenericRepository<PaymentTransaction, int>, IPaymentTransactionRepository
{
    private readonly ExpenseFlowDbContext _context;

    public PaymentTransactionRepository(ExpenseFlowDbContext context) : base(context)
    {
        _context = context;
    }
}

