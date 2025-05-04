using ExpenseFlow.DataAccess.GenericRepository;

namespace ExpenseFlow.DataAccess.ExpenseCategories;

public interface IExpenseCategoryRepository : IGenericRepository<ExpenseCategory, int>
{
}

