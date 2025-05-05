using ExpenseFlow.DataAccess.GenericRepository;

namespace ExpenseFlow.DataAccess.PaymentTransactions;

public interface IPaymentTransactionRepository : IGenericRepository<PaymentTransaction, int>
{

}

