using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configuration;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("stores");

        builder.Property(p => p.Name)
            .IsRequired();

        builder.Property(p => p.Description)
            .IsRequired();

        builder.Property(p => p.Address)
            .IsRequired();

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.HasMany(p => p.Products)
            .WithMany(p => p.Stores)
            .UsingEntity<StoreProducts>(
            j => j.HasOne(pt => pt.Product)
            .WithMany(t => t.StoreProducts)
            .HasForeignKey(pt => pt.ProductId),
            j => j.HasOne(pt => pt.Store)
            .WithMany(t => t.StoreProducts)
            .HasForeignKey(pt => pt.StoreId),
            j =>
            {
               
                j.HasKey(t => new { t.ProductId, t.StoreId });
            }
            );


        builder.HasMany(p => p.Products)
            .WithMany(p => p.Stores)
            .UsingEntity<Alert>(
            j => j.HasOne(pt => pt.Product)
            .WithMany(t => t.Alerts)
            .HasForeignKey(pt => pt.ProductId),
            j => j.HasOne(pt => pt.Store)
            .WithMany(t => t.Alerts)
            .HasForeignKey(pt => pt.StoreId),
            j =>
            {
                j.HasKey(t => new { t.ProductId, t.StoreId });
            }
            );

    }
}
