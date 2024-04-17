using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => new { p.CreditProductId, p.CreditCardProductId, p.CurrentAccountProductId });

            entity
                 .HasOne(e => e.CreditProduct)
                 .WithMany(e => e.products)
                 .HasForeignKey(e => e.CreditProductId);

            entity
                 .HasOne(e => e.CreditCardProduct)
                 .WithMany(e => e.products)
                 .HasForeignKey(e => e.CreditCardProductId);
            entity
                 .HasOne(e => e.CurrentAccountProduct)
                 .WithMany(e => e.products)
                 .HasForeignKey(e => e.CurrentAccountProductId);
        }
    }
}


