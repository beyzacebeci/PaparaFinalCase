using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.ExpenseClaims;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpenseFlow.DataAccess;

public class ExpenseFlowDbContext : DbContext 
{
    public ExpenseFlowDbContext(DbContextOptions<ExpenseFlowDbContext> options) : base(options)
    {        

    }
    public DbSet<ExpenseClaim> ExpenseClaim { get; set; } = default!;
    public DbSet<ExpenseCategory> ExpenseCategory { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

}

