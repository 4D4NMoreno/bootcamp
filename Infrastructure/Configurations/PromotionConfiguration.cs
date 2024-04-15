using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Core.Data.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions"); // Nombre de la tabla en la base de datos

            builder.HasKey(p => p.Id); // Clave primaria

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100); // Longitud máxima de 100 caractere

            builder
               .HasOne(p => p.Enterprise)
            .WithMany(p => p.Promotions)
            .HasForeignKey(d => d.EnterpriseId);


        }
    }
}