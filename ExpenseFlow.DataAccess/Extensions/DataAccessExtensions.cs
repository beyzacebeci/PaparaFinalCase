using ExpenseFlow.DataAccess.AppUnitOfWork;
using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.ExpenseClaims;
using ExpenseFlow.DataAccess.GenericRepository;
using ExpenseFlow.DataAccess.PaymentTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ExpenseFlow.DataAccess.Extensions;

public static class DataAccessExtensions
{
    public static IServiceCollection AddDataAccesses(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbContext<ExpenseFlowDbContext>(options =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            
            options.UseSqlServer(connectionStrings!.SqlServer,sqlServerOptionsAction =>
            {
               sqlServerOptionsAction.MigrationsAssembly(typeof(DataAccessAssembly).Assembly.FullName);
            });     
        });

        services.AddScoped<IExpenseClaimRepository, ExpenseClaimRepository>();
        services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
        services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();

        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;    
    }
}
