using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CurrentAccountConfiguration : IEntityTypeConfiguration<CurrentAccount>
    {
        public void Configure(EntityTypeBuilder<CurrentAccount> entity)
        {
            entity.HasKey(e => e.Id).HasName("CurrentAccount_pkey");

            entity.Property(e => e.OperationalLimit).HasColumnType("decimal(20, 5)");
            entity.Property(e => e.MonthAverage).HasColumnType("decimal(20, 5)");
            entity.Property(e => e.Interest).HasColumnType("decimal(10, 5)");

            entity
                .HasOne(CurrentAccount => CurrentAccount.Account)
                .WithMany(Account => Account.CurrentAccounts)
                .HasForeignKey(CurrentAccount => CurrentAccount.AccountId);
        }
    }
}