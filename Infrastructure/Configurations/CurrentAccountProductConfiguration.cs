using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CurrentAccountProductConfiguration : IEntityTypeConfiguration<CurrentAccountProduct>
    {
        public void Configure(EntityTypeBuilder<CurrentAccountProduct> builder)
        {
            builder.ToTable("CurrentAccountProduct");

            builder
                .HasKey(cap => cap.Id)
                .HasName("CurrentAccountProduct_pkey");

            builder
                .Property(cap => cap.DepositAmount)
                .HasColumnType("numeric(20,5)"); 

            builder
                .Property(cap => cap.RequestDate)
                .IsRequired();

            builder
                .Property(cap => cap.ApprovalDate)
                .IsRequired();

            builder
                .HasOne(cap => cap.Currency)
                .WithMany()
                .HasForeignKey(cap => cap.CurrencyId);

            builder
                .HasOne(cap => cap.Bank)
                .WithMany()
                .HasForeignKey(cap => cap.BankId);

            builder
                .HasOne(cap => cap.Customer)
                .WithMany()
                .HasForeignKey(cap => cap.CustomerId);

            builder
            .HasOne(d => d.product)
            .WithOne(p => p.CurrentAccountProduct)
            .HasForeignKey<CurrentAccountProduct>(d => d.ProductId);


        }
    }
}
