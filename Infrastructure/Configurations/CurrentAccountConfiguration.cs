using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CurrentAccountConfiguration : IEntityTypeConfiguration<CurrentAccount>
    {
        public void Configure(EntityTypeBuilder<CurrentAccount> entity)
        {
            entity.ToTable("CurrentAccount");

            entity.HasKey(e => e.Id).HasName("CurrentAccount_pkey");

            entity.Property(e => e.OperationalLimit).HasMaxLength(400).IsRequired();
            entity.Property(e => e.MonthAverage).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Interest).HasMaxLength(300).IsRequired();

            entity
            .HasOne(CurrentAccount => CurrentAccount.Account)
            .WithMany(Account => Account.CurrentAccounts)
            .HasForeignKey(Account => Account.AccountId);
        }
    }
}