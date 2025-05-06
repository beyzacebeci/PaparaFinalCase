using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseFlow.DataAccess.ExpenseCategories;

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
        {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.ExpenseClaims)
            .WithOne(x => x.ExpenseCategory)
            .HasForeignKey(x => x.ExpenseCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasData(
            new ExpenseCategory { Id = 1, Name = "Ulaşım", CreatedDate = new DateTime(2025, 5, 6), UpdatedDate = new DateTime(2025, 5, 6) },
            new ExpenseCategory { Id = 2, Name = "Yemek", CreatedDate = new DateTime(2025, 5, 6), UpdatedDate = new DateTime(2025, 5, 6) },
            new ExpenseCategory { Id = 3, Name = "Ofis Malzemeleri", CreatedDate = new DateTime(2025, 5, 6), UpdatedDate = new DateTime(2025, 5, 6) }
        );
    }
}

