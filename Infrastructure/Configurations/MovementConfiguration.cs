using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Constants;
using Core.Entities;

namespace Core.EntityConfigurations
{
    public class MovementConfiguration : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> entity)
        {
            entity.HasKey(e => e.Id).HasName("Movement_pkey");

            entity.Property(e => e.Destination).HasMaxLength(150);
            entity.Property(e => e.Amount).HasPrecision(20, 5);
            entity.Property(e => e.TransferredDateTime).HasColumnType("Date");
            entity.Property(e => e.TransferStatus).HasMaxLength(50);

            entity.HasOne(Movement => Movement.Account)
                   .WithMany(Account => Account.Movements)
                   .HasForeignKey(Movement => Movement.AccountId);
        }
    }
}