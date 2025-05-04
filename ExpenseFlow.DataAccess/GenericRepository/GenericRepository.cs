using ExpenseFlow.DataAccess.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExpenseFlow.DataAccess;

public class GenericRepository<T, TId> : IGenericRepository<T, TId> where T
    : BaseEntity<TId> where TId : struct
{
    protected readonly ExpenseFlowDbContext Context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(ExpenseFlowDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }


    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x => x.Id.Equals(id));


    public IQueryable<T> GetAll() => _dbSet.Where(x => x.IsActive).AsQueryable().AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) =>
        _dbSet.Where(x => x.IsActive).Where(predicate).AsNoTracking();

    public async ValueTask<T?> GetByIdAsync(TId id)
    {
        return await _dbSet.Where(x => x.IsActive && x.Id.Equals(id))
                           .FirstOrDefaultAsync();
    }

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity)
    {
        // Soft delete implementation
        entity.IsActive = false;
        _dbSet.Update(entity);
    }
}


