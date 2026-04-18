using Microsoft.EntityFrameworkCore;
using NorthwindCatalog.Services.Models;

namespace NorthwindCatalog.Services.Data;

public class NorthwindContext : DbContext
{
    public NorthwindContext(DbContextOptions<NorthwindContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(15);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.Picture).HasColumnType("image");

            // Configure one-to-many relationship
            entity.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure Product entity
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);
            entity.Property(e => e.ProductId).ValueGeneratedOnAdd();
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(40);
            entity.Property(e => e.UnitPrice).HasPrecision(10, 2).HasDefaultValue(0);
            entity.Property(e => e.UnitsInStock).HasDefaultValue(0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue(0);
            entity.Property(e => e.ReorderLevel).HasDefaultValue(0);
            entity.Property(e => e.Discontinued).HasDefaultValue(false);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(20);

            // Configure foreign key
            entity.HasOne(e => e.Category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Configure table names to match SQL Server conventions
        modelBuilder.Entity<Category>().ToTable("Categories");
        modelBuilder.Entity<Product>().ToTable("Products");
    }
}
