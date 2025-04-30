namespace ExpenseFlow.DataAccess.AppUnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ExpenseFlowDbContext _context;

    public UnitOfWork(ExpenseFlowDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
