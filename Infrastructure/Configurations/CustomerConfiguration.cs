using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CustomersConfiguration : IEntityTypeConfiguration<Customers>
    {
        public void Configure(EntityTypeBuilder<Customers> entity)
        {
            entity.ToTable("Customers");
            entity.HasKey(e => e.Id).HasName("Customers_pkey");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Lastname).HasPrecision(20, 5);
            entity.Property(e => e.DocumentNumber).HasMaxLength(100);
            entity.Property(e => e.Address).HasPrecision(20, 5);
            entity.Property(e => e.Mail).HasMaxLength(100);
            entity.Property(e => e.Phone).HasPrecision(20, 5);

            entity
             .HasOne(customers => customers.Bank)
            .WithMany(Bank => Bank.Customers)
            .HasForeignKey(Bank => Bank.BankId);

        }
    }
}