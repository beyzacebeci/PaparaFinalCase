namespace ExpenseFlow.Application.ExpenseClaims;

public interface IExpenseClaimService
{
    Task<ServiceResult<List<ExpenseClaimResponse>>> GetAllListAsync();
    Task<ServiceResult<ExpenseClaimResponse?>> GetByIdAsync(int id);
    Task<ServiceResult<ExpenseClaimResponse>> CreateAsync(ExpenseClaimRequest request);
    Task<ServiceResult> UpdateAsync(int id, ExpenseClaimRequest request);
    Task<ServiceResult> DeleteAsync(int id);
}

