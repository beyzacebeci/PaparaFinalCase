using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.GenericRepository;

namespace ExpenseFlow.DataAccess.ExpenseCategories;

public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory, int>
{
    Task<List<ExpenseCategory>> GetAllIncludingClaimsAsync();
}

