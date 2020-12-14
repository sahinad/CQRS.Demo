using CQRS.Demo.Domain.Configurations;
using CQRS.Demo.Domain.Views;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Demo.Domain.Models
{
    public class DomainContext : DbContext
    {
        public DomainContext(DbContextOptions options) : base(options)
        {
        }

        #region Tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        #endregion

        #region Views

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());

            modelBuilder.Entity<CurrentProductListView>(x =>
            {
                x.HasKey(y => y.Identifier);
                x.ToView("CurrentProductListView", "dbo");
            });

            modelBuilder.Entity<ProductsByCategoryView>(x =>
            {
                x.HasKey(y => y.Identifier);
                x.ToView("ProductsByCategoryView", "dbo");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
