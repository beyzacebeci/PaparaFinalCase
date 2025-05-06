using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace ExpenseFlow.DataAccess.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Balance)
       .HasPrecision(18, 2);

        var hasher = new PasswordHasher<User>();
            // Admin Password: Admin2024.
            // Employee Password: Employee2024.
            builder.HasData(
            new User
            {
                Id = "1",
                FirstName = "Admin",
                LastName = "Admin",
                Email ="admin@gmail.com",
                UserName = "admin",
                IBAN = "TR000100200300400500600004",
                Balance = 2345,
                NormalizedUserName = "ADMIN",
                PasswordHash = "AQAAAAEAACcQAAAAEEf2DGvVu8CMOcRhLn6V9ksInCCiM2rSw+aSuoOGNrBLK7KaQaQNhm+kGeBr3dWr0g==", 
                SecurityStamp = "admin-security-stamp",
                ConcurrencyStamp = "admin-concurrency-stamp"
            },
            new User
            {
                Id = "2",
                FirstName = "Employee",
                LastName = "Employee",
                Email ="user@gmail.com",
                UserName = "employee",
                IBAN = "TR000100200300400500600000",
                Balance = 2345,
                NormalizedUserName = "EMPLOYEE",
                PasswordHash = "AQAAAAEAACcQAAAAEBaO23+qroKgH+c5jkG2EOeual/QDtvxvzcci35GoY/Yt+hPKhaGNuCt37b/Xpsl2A==", 
                SecurityStamp = "admin-security-stamp",
                ConcurrencyStamp = "admin-concurrency-stamp"
            }
        );
    }
}
public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasData(
        new UserRole
        {
            Id = "1",
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new UserRole
        {
            Id = "2",
            Name = "Employee",
            NormalizedName = "EMPLOYEE"
        }
        );
    }
}
public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
        new IdentityUserRole<string>
        {
            UserId = "1",
            RoleId = "1"
        },
        new IdentityUserRole<string>
        {
            UserId = "2",
            RoleId = "2"
        }
        );
    }
}