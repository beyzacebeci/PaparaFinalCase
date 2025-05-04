    using ExpenseFlow.Application.ExceptionHandlers;
    using ExpenseFlow.Application.ExpenseClaims;
using ExpenseFlow.Application.Users;
using ExpenseFlow.DataAccess;
    using ExpenseFlow.DataAccess.Users;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using System.Reflection;
    using System.Text;

    namespace ExpenseFlow.Application.Extensions;

    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplications(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IExpenseClaimService, ExpenseClaimService>();
       
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ExpenseFlowDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityErrorDescriber>();

            // JWT konfigürasyonunu burada ekliyoruz
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            });

            return services;
        }
    }

