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
        return services;
    }
}
