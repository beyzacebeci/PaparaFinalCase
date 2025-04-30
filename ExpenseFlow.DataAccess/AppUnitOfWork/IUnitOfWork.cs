namespace ExpenseFlow.DataAccess.AppUnitOfWork;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}

