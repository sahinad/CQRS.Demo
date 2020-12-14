using CQRS.Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Demo.Domain.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CompanyName).IsRequired().HasMaxLength(40);
            builder.Property(x => x.ContactName).HasMaxLength(30);
            builder.Property(x => x.ContactTitle).HasMaxLength(30);
            builder.Property(x => x.Address).HasMaxLength(60);
            builder.Property(x => x.City).HasMaxLength(15);
            builder.Property(x => x.Region).HasMaxLength(15);
            builder.Property(x => x.PostalCode).HasMaxLength(15);
            builder.Property(x => x.Country).HasMaxLength(15);
            builder.Property(x => x.Phone).HasMaxLength(24);
            builder.Property(x => x.Fax).HasMaxLength(24);
            builder.Property(x => x.HomePage).HasMaxLength(500);
            builder.HasIndex(x => x.CompanyName).IsUnique();
        }
    }
}
