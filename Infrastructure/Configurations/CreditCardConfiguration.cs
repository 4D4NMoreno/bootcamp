using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> entity)
        {
            entity.ToTable("CreditCards");

            entity
                .HasKey(e => e.Id)
                .HasName("CreditCard_pkey");

            entity
                .Property(e => e.Designation)
                .HasMaxLength(50);

            entity
                .Property(e => e.CardNumber)
                .IsRequired();

            entity
                .Property(e => e.CVV)
                .IsRequired();

            entity
                .Property(e => e.CreditLimit)
                .HasColumnType("numeric(20,5)")
                .IsRequired();

            entity
                .Property(e => e.AvaibleCredit)
                .HasColumnType("numeric(20,5)")
                .IsRequired();

            entity
                .Property(e => e.CurrentDebt)
                .HasColumnType("numeric(20,5)")
                .IsRequired();

            entity
                .Property(e => e.InterestRate)
                .HasColumnType("numeric(20,5)")
                .IsRequired();

            entity
                .Property(e => e.IssueDate)
                .HasColumnType("date")
                .IsRequired();

            entity
                .Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .IsRequired();

            entity
                .HasOne(x => x.Customer)
                .WithMany(x => x.CreditCards)
                .HasForeignKey(x => x.CustomerId);

            entity
                .HasOne(x => x.Currency)
                .WithMany(x => x.CreditCards)
                .HasForeignKey(x => x.CurrencyId);

        }
    }
}