using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> entity)
        {
            entity.ToTable("Service");

            entity.HasKey(e => e.Id).HasName("Service_pkey");

            entity.Property(e => e.ServiceName)
                  .HasMaxLength(100)
                  .IsRequired();
            
        }
    }
}