using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NavQurt.Server.Domain.Entities;

namespace NavQurt.Server.Infrastructure.Persistence.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Title)
               .HasMaxLength(200)
               .IsRequired();

        builder.Property(w => w.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(w => w.IsDeleted)
               .IsRequired()
               .HasDefaultValue(false);
    }
}