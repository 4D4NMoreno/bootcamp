using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity
            .HasKey(p => p.Id)
            .HasName("Product_pkey");

        entity
            .Property(e => e.ProductName)
            .HasMaxLength(150)
            .IsRequired();

        entity
            .HasMany(d => d.ProductRequests)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId);
    }
}