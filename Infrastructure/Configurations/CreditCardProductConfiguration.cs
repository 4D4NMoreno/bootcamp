using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CreditCardProductConfiguration : IEntityTypeConfiguration<CreditCardProduct>
    {
        public void Configure(EntityTypeBuilder<CreditCardProduct> builder)
        {
            builder.ToTable("CreditCardProduct");

            builder
                .HasKey(ccp => ccp.Id)
                .HasName("CreditCardProduct_pkey");

            builder
                .Property(ccp => ccp.Brand)
                .HasMaxLength(100);

            builder
                .Property(ccp => ccp.RequestDate)
                .IsRequired();

            builder
                .Property(ccp => ccp.ApprovalDate)
                .IsRequired();

            builder
                .HasOne(ccp => ccp.Currency)
                .WithMany()
                .HasForeignKey(ccp => ccp.CurrencyId);

            builder
                .HasOne(ccp => ccp.Bank)
                .WithMany()
                .HasForeignKey(ccp => ccp.BankId);

            builder
                .HasOne(ccp => ccp.Customer)
                .WithMany()
                .HasForeignKey(ccp => ccp.CustomerId);

            builder
            .HasOne(d => d.product)
            .WithOne(p => p.CreditCardProduct)
            .HasForeignKey<CreditCardProduct>(d => d.ProductId);


        }
    }
}
