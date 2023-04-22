using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.Property(p=>p.Name)
            .IsRequired();

        builder.Property(p=>p.Description)
            .IsRequired();

        builder.Property(p=>p.Amount)
            .IsRequired();

        builder.Property(p => p.Temperature)
            .IsRequired();

    }
}
