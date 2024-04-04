using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Constants;
using Core.Entities;

namespace Core.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.HasKey(e => e.Id).HasName("Customer_pkey");

            entity.Property(e => e.Name).HasMaxLength(300).IsRequired();
            entity.Property(e => e.Lastname).HasMaxLength(300);
            entity.Property(e => e.DocumentNumber).HasMaxLength(150).IsRequired();
            entity.Property(e => e.Address).HasMaxLength(400);
            entity.Property(e => e.Mail).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(150);
            entity.Property(e => e.Estado).HasMaxLength(100);


            entity.HasOne(Customer => Customer.Bank)
                   .WithMany(Bank => Bank.Customers)
                   .HasForeignKey(Bank => Bank.BankId);
        }
    }
}