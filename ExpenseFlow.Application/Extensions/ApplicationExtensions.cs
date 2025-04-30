using ExpenseFlow.Application.ExpenseClaims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExpenseFlow.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IExpenseClaimService, ExpenseClaimService>();
       
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}

