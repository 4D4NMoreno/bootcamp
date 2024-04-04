using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Constants;
using Core.Entities;

namespace Core.EntityConfigurations
{
    public class SavingAccountConfiguration : IEntityTypeConfiguration<SavingAccount>
    {
        public void Configure(EntityTypeBuilder<SavingAccount> entity)
        {
            entity.HasKey(e => e.Id).HasName("SavingAccount_pkey");

            entity.Property(e => e.HolderName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.SavingType).HasMaxLength(50);
            entity.Property(e => e.AccountId);


            entity.HasOne(SavingAccount => SavingAccount.Account)
                   .WithMany(Account => Account.SavingAccounts)
                   .HasForeignKey(SavingAccount => SavingAccount.AccountId);
        }
    }
}