using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductRequest>
    {
        public void Configure(EntityTypeBuilder<ProductRequest> entity)
        {
            entity.HasKey(p => p.Id);

            
        }
    }
}


