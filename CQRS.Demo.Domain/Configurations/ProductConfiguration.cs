using CQRS.Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Demo.Domain.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.UnitPrice).HasPrecision(6, 2);
            builder.Property(x => x.QuantityPerUnit).HasMaxLength(20);
            builder.HasIndex(x => x.ProductName).IsUnique();
        }
    }
}
