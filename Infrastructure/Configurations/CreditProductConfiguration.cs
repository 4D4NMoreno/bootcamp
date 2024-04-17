using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CreditProductConfiguration : IEntityTypeConfiguration<CreditProduct>
    {
        public void Configure(EntityTypeBuilder<CreditProduct> builder)
        {
            builder.ToTable("CreditProduct");

            builder
                .HasKey(cp => cp.Id)
                .HasName("CreditProduct_pkey");

            builder
                .Property(cp => cp.Amount)
                .HasColumnType("numeric(20,5)"); 

            builder
                .Property(cp => cp.RequestDate)
                .IsRequired();

            builder
                .Property(cp => cp.ApprovalDate)
                .IsRequired();

            builder
                .Property(cp => cp.Term)
                .IsRequired(); 

            builder
                .HasOne(cp => cp.Currency)
                .WithMany()
                .HasForeignKey(cp => cp.CurrencyId);

            builder
                .HasOne(cp => cp.Bank)
                .WithMany()
                .HasForeignKey(cp => cp.BankId);

            builder
                .HasOne(cp => cp.Customers)
                .WithMany()
                .HasForeignKey(cp => cp.CustomerId);

        }
    }
}
