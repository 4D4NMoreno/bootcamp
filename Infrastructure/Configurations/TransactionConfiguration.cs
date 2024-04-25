using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(t => t.TransactionDateTime).IsRequired();

            builder.Property(t => t.DestinationAccountNumber).HasMaxLength(100);

            builder.Property(t => t.DestinationDocumentNumber).HasMaxLength(50);
            builder.Property(e => e.OriginAccountId)
                   .HasColumnName("OriginAccountId");


            builder.Property(e => e.Bank)
                   .HasColumnName("Bank");

            //builder.Property(e => e.TransactionType)
            //       .HasColumnName("TransactionType");

        }
    }
}
