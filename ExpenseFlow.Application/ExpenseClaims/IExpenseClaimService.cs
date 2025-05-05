using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.Application.ExpenseClaims;

public interface IExpenseClaimService
{
    Task<ServiceResult<List<ExpenseClaimResponse>>> GetAllListAsync();
    Task<ServiceResult<ExpenseClaimResponse?>> GetByIdAsync(int id);
    Task<ServiceResult> CreateAsync(ExpenseClaimRequest request);
    Task<ServiceResult> UpdateAsync(int id, ExpenseClaimRequest request);
    Task<ServiceResult> DeleteAsync(int id);
    Task<ServiceResult<List<ExpenseClaimResponse>>> GetListByCurrentUserAsync();
    Task<ServiceResult> UpdateExpenseClaimStatusAsync(int id, ExpenseStatus newStatus, string? statusDescription = null);

}

