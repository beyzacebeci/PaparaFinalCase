using ExpenseFlow.Application.ExpenseClaims;

namespace ExpenseFlow.Application.ExpenseCategories;

public interface IExpenseCategoryService
{
    Task<ServiceResult<List<ExpenseCategoryResponse>>> GetAllListAsync();
    Task<ServiceResult<ExpenseCategoryResponse?>> GetByIdAsync(int id);
    Task<ServiceResult> CreateAsync(ExpenseCategoryRequest request);
    Task<ServiceResult> UpdateAsync(int id, ExpenseCategoryRequest request);
    Task<ServiceResult> DeleteAsync(int id);
}

