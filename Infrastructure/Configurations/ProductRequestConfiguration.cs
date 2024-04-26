using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ProductRequestConfiguration : IEntityTypeConfiguration<ProductRequest>
{
    public void Configure(EntityTypeBuilder<ProductRequest> entity)
    {
        entity.HasKey(e => e.Id).HasName("Request_pkey");

        entity
            .Property(e => e.Description)
            .HasMaxLength(100)
            .IsRequired();

        entity
            .HasOne(request => request.Currency)
            .WithMany(currency => currency.ProductRequets)
            .HasForeignKey(request => request.CurrencyId);

        entity
            .HasOne(request => request.Customer)
            .WithMany(customer => customer.ProductRequets)
            .HasForeignKey(request => request.CustomerId);

        entity
            .HasOne(request => request.Product)
            .WithMany(product => product.ProductRequests)
            .HasForeignKey(request => request.ProductId);
    }
}


