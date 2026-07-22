using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;

namespace RetailInventory.Data;

// Lab 2: AppDbContext — the bridge between C# models and SQL Server tables
// ORM benefit: No hand-written SQL needed; EF generates it from model definitions
public class AppDbContext : DbContext
{
    // Lab 2: DbSet properties map to tables in the database
    public DbSet<Product>       Products       { get; set; }
    public DbSet<Category>      Categories     { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<Tag>           Tags           { get; set; }

    // Lab 2: Configure the SQL Server connection
    // Lab 10: UseLazyLoadingProxies() enables lazy loading — navigation properties
    //         loaded automatically when accessed (requires virtual keyword on nav props)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            // Lab 10: Lazy Loading — comment out if not needed (adds overhead)
            .UseLazyLoadingProxies()
            .UseSqlServer(
                "Server=localhost;Database=RetailInventoryDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Lab 11: One-to-one — Product has one ProductDetail
        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductDetail)
            .WithOne(pd => pd.Product)
            .HasForeignKey<ProductDetail>(pd => pd.ProductId);

        // Lab 11: Many-to-many — EF Core 8 configures the join table automatically
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Tags)
            .WithMany(t => t.Products);

        modelBuilder.Entity<Category>()
            .HasMany(c => c.Tags)
            .WithMany(t => t.Categories);

        // Lab 9: Seed initial data with HasData()
        // HasData requires explicit IDs — EF uses them to detect add vs update during migration
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Groceries"   }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Smartphone",  Price = 25000, CategoryId = 1, StockQuantity = 50  },
            new Product { Id = 2, Name = "Wheat Flour",  Price = 800,  CategoryId = 2, StockQuantity = 100 }
        );
    }
}
