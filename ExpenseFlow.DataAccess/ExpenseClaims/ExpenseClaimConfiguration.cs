using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpenseFlow.DataAccess.ExpenseClaims;

public class ExpenseClaimConfiguration : IEntityTypeConfiguration<ExpenseClaim>
{
    public void Configure(EntityTypeBuilder<ExpenseClaim> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Location)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.ExpenseDate)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ExpenseStatusDescription)
            .HasMaxLength(500);

        builder.Property(x => x.PaymentMethod)
            .IsRequired();

        builder.Property(x => x.PaymentReference)
            .HasMaxLength(255);

        builder.Property(x => x.ApprovalDate)
            .IsRequired(false);

        builder.HasOne(x => x.ExpenseCategory)
            .WithMany(x => x.ExpenseClaims)
            .HasForeignKey(x => x.ExpenseCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new ExpenseClaim
            {
                Id = 1,
                ExpenseCategoryId = 1,
                UserId = "2",
                Amount = 320.00m,
                Description = "Toplantı için şehir dışı ulaşım gideri",
                Location = "İstanbul",
                ExpenseDate = new DateTime(2025, 4, 10),  
                Status = ExpenseStatus.Approved,
                ExpenseStatusDescription = "Yönetici tarafından onaylandı",
                PaymentMethod = PaymentMethod.CreditCard,
                PaymentReference = "ODENE123",
                ApprovalDate = new DateTime(2025, 4, 11),  
                CreatedDate = new DateTime(2025, 4, 10),  
                UpdatedDate = new DateTime(2025, 4, 10)  
            },
            new ExpenseClaim
            {
                Id = 2,
                ExpenseCategoryId = 2,
                UserId = "2",
                Amount = 85.50m,
                Description = "Müşteri ile öğle yemeği",
                Location = "Ankara",
                ExpenseDate = new DateTime(2025, 4, 15),  
                Status = ExpenseStatus.Pending,
                ExpenseStatusDescription = "Beklemede.",
                PaymentMethod = PaymentMethod.Cash,
                CreatedDate = new DateTime(2025, 4, 15),  
                UpdatedDate = new DateTime(2025, 4, 15)   
            },
            new ExpenseClaim
            {
                Id = 3,
                ExpenseCategoryId = 3,
                UserId = "2",
                Amount = 210.75m,
                Description = "Yeni yazıcı kartuşu ve kırtasiye giderleri",
                Location = "İzmir",
                ExpenseDate = new DateTime(2025, 4, 18),  
                Status = ExpenseStatus.Rejected,
                ExpenseStatusDescription = "Fatura eksikliği nedeniyle reddedildi",
                PaymentMethod = PaymentMethod.DebitCard,
                PaymentReference = "FIS789",
                CreatedDate = new DateTime(2025, 4, 18),  
                UpdatedDate = new DateTime(2025, 4, 18)  
            },
            new ExpenseClaim
            {
                Id = 4,
                ExpenseCategoryId = 1,
                UserId = "1",
                Amount = 1250.00m,
                Description = "İstanbul müşteri ziyareti için uçak bileti",
                Location = "İstanbul",
                ExpenseDate = new DateTime(2025, 3, 15),  
                Status = ExpenseStatus.Approved,
                ExpenseStatusDescription = "Yönetici onayı alındı",
                PaymentMethod = PaymentMethod.CreditCard,
                PaymentReference = "FLIGHT123",
                ApprovalDate = new DateTime(2025, 3, 20),  
                CreatedDate = new DateTime(2025, 3, 15),  
                UpdatedDate = new DateTime(2025, 3, 15)   
            },
            new ExpenseClaim
            {
                Id = 5,
                ExpenseCategoryId = 2,
                UserId = "1",
                Amount = 89.99m,
                Description = "Ofis kahve makinesi için temizlik malzemeleri",
                Location = "Ankara",
                ExpenseDate = new DateTime(2025, 4, 5),  
                Status = ExpenseStatus.Paid,
                ExpenseStatusDescription = "Ödeme tamamlandı",
                PaymentMethod = PaymentMethod.Cash,
                PaymentReference = "CASH456",
                ApprovalDate = new DateTime(2025, 4, 6),  
                CreatedDate = new DateTime(2025, 4, 5),  
                UpdatedDate = new DateTime(2025, 4, 5) 
            }
        );

    }
}

