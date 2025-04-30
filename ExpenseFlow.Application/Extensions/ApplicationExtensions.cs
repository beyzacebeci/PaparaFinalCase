using ExpenseFlow.Application.ExpenseClaims;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseFlow.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddScoped<IExpenseClaimService, ExpenseClaimService>();
        return services;





    }
}

