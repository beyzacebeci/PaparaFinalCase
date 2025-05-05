using ExpenseFlow.Application.ExpenseCategories;

namespace ExpenseFlow.Application.PaymentTransactions;

public interface IPaymentTransactionService
{
    Task<ServiceResult<List<PaymentTransactionResponse>>> GetAllListAsync();
    Task<ServiceResult> CreateAsync(PaymentTransactionRequest request);
}

