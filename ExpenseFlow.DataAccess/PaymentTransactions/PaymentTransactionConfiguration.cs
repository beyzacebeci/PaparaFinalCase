using ExpenseFlow.DataAccess.ExpenseClaims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseFlow.DataAccess.PaymentTransactions;

public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
{
    public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
    { 

        builder.HasKey(pt => pt.Id);

        builder.Property(pt => pt.Amount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(pt => pt.PaymentDate)
            .IsRequired();

        builder.Property(pt => pt.PaymentReference)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(pt => pt.PaymentStatus)
            .HasMaxLength(20)
            .IsRequired();
    }
}
