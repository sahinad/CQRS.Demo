using CQRS.Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRS.Demo.Domain.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.HasIndex(x => x.CategoryName).IsUnique();
        }
    }
}
