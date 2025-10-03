using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
               .HasMaxLength(200)
               .IsRequired(false);

        builder.Property(p => p.Priority)
               .IsRequired();

        builder.Property(p => p.SelfPrice)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(p => p.Price)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(p => p.InSale)
               .IsRequired();

        builder.Property(p => p.IsCombo)
               .IsRequired();

        builder.Property(p => p.ProductPhotoPath)
               .HasMaxLength(255);

        builder.Property(p => p.ImageHash)
               .HasMaxLength(64);

        builder.HasOne(p => p.ProductCategory)
               .WithMany(pc => pc.Products)
               .HasForeignKey(p => p.ProductCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Warehouse)
               .WithMany()
               .HasForeignKey(p => p.WarehouseId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}