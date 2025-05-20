using Ecommerce.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<State>()
                .HasIndex(s => new { s.Name, s.CountryId })
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(c => new { c.Name, c.StateId })
                .IsUnique();

            modelBuilder.Entity<ProductCategory>()
                .HasIndex(pc => new { pc.Name, pc.CategoryId })
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name, p.ProductCategoryId })
                .IsUnique();

            modelBuilder.Entity<ProductImage>()
                .HasIndex(pi => new { pi.Image, pi.ProductId })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.Document, u.CityId })
                .IsUnique();
        }
    }
}