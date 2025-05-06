using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.ExpenseClaims;
using ExpenseFlow.DataAccess.PaymentTransactions;
using ExpenseFlow.DataAccess.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ExpenseFlow.DataAccess;

public class ExpenseFlowDbContext : IdentityDbContext<User>
{

    public ExpenseFlowDbContext(DbContextOptions<ExpenseFlowDbContext> options) : base(options)
    {
    }

    public DbSet<ExpenseClaim> ExpenseClaim { get; set; } = default!;
    public DbSet<ExpenseCategory> ExpenseCategory { get; set; } = default!;
    public DbSet<PaymentTransaction> PaymentTransaction { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

