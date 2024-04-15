using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Data.Configurations
{
    public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.ToTable("Enterprises");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Phone)
                .HasMaxLength(20);

            builder.Property(e => e.Mail)
                .HasMaxLength(100);


            builder.HasMany(e => e.Promotions)
                .WithOne(p => p.Enterprise)
                .HasForeignKey(p => p.EnterpriseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}