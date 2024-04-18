using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);

            entity
            .HasOne(product => product.CreditProduct)
            .WithOne(CreditProduct => CreditProduct.product)
            .HasForeignKey<CreditProduct>(CreditProduct => CreditProduct.ProductId);

            entity
            .HasOne(product => product.CreditCardProduct)
            .WithOne(CreditProduct => CreditProduct.product)
            .HasForeignKey<CreditProduct>(CreditProduct => CreditProduct.ProductId);

            entity
            .HasOne(product => product.CurrentAccountProduct)
            .WithOne(CreditProduct => CreditProduct.product)
            .HasForeignKey<CreditProduct>(CreditProduct => CreditProduct.ProductId);
        }
    }
}


