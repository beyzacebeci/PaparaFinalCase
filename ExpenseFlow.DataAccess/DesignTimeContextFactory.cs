using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ExpenseFlow.DataAccess;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ExpenseFlowDbContext>
{
    public ExpenseFlowDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExpenseFlowDbContext>();

        // Connection string'i buradan al
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

        optionsBuilder.UseSqlServer(connectionStrings.SqlServer, sqlServerOptionsAction =>
        {
            sqlServerOptionsAction.MigrationsAssembly(typeof(DataAccessAssembly).Assembly.FullName);
        });

        return new ExpenseFlowDbContext(optionsBuilder.Options);
    }
}