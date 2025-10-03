using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");

        builder.HasKey(pc => pc.Id);

        builder.Property(pc => pc.Title)
               .HasMaxLength(200)
               .IsRequired(false);

        builder.Property(pc => pc.IsDeleted)
               .IsRequired();

        builder.Property(pc => pc.InSale)
               .IsRequired();

        builder.Property(pc => pc.Priority)
               .IsRequired();

        builder.Property(pc => pc.ProductsPerRow)
               .IsRequired();

        builder.Property(pc => pc.ImageHash)
               .HasMaxLength(64);

        builder.Property(pc => pc.ProductCategoryPhotoPath)
               .HasMaxLength(255);

        builder.HasOne(pc => pc.ParentCategory)
               .WithMany()
               .HasForeignKey(pc => pc.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}