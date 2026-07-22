using Microsoft.EntityFrameworkCore;
using RetailInventory.Data;
using RetailInventory.DTOs;
using RetailInventory.Models;
using EFCore.BulkExtensions;

// ============================================================
// NOTE: This program demonstrates all 15 EF Core 8 labs.
// Each lab is clearly labelled. To run a specific lab,
// uncomment its section and comment out the others.
// Requires SQL Server running with RetailInventoryDB created
// via: dotnet ef database update
// ============================================================

Console.WriteLine("=== EF Core 8 Retail Inventory System ===\n");

// ============================================================
// LAB 4: Insert Initial Data
// Objective: Use AddRangeAsync and SaveChangesAsync to insert records
// ============================================================
static async Task Lab4_InsertData()
{
    Console.WriteLine("--- Lab 4: Insert Initial Data ---");
    using var context = new AppDbContext();

    var electronics = new Category { Name = "Electronics" };
    var groceries   = new Category { Name = "Groceries" };
    await context.Categories.AddRangeAsync(electronics, groceries);

    var product1 = new Product { Name = "Laptop",    Price = 75000, Category = electronics, StockQuantity = 10 };
    var product2 = new Product { Name = "Rice Bag",  Price = 1200,  Category = groceries,   StockQuantity = 50 };
    await context.Products.AddRangeAsync(product1, product2);

    await context.SaveChangesAsync();
    Console.WriteLine("Data inserted successfully.\n");
}

// ============================================================
// LAB 5: Retrieve Data
// Objective: Use ToListAsync, FindAsync, FirstOrDefaultAsync
// ============================================================
static async Task Lab5_RetrieveData()
{
    Console.WriteLine("--- Lab 5: Retrieve Data ---");
    using var context = new AppDbContext();

    // ToListAsync — fetch all products
    var products = await context.Products.ToListAsync();
    Console.WriteLine("All Products:");
    foreach (var p in products)
        Console.WriteLine($"  {p.Name} - Rs.{p.Price}");

    // FindAsync — fetch by primary key (most efficient, checks cache first)
    var found = await context.Products.FindAsync(1);
    Console.WriteLine($"\nFound by ID 1: {found?.Name}");

    // FirstOrDefaultAsync with condition
    var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
    Console.WriteLine($"Expensive product (>50000): {expensive?.Name}\n");
}

// ============================================================
// LAB 6: Update and Delete Records
// Objective: Modify tracked entities and call SaveChangesAsync
// ============================================================
static async Task Lab6_UpdateDelete()
{
    Console.WriteLine("--- Lab 6: Update and Delete ---");
    using var context = new AppDbContext();

    // Update — EF tracks changes to fetched entities; SaveChangesAsync generates UPDATE SQL
    var product = await context.Products.FirstOrDefaultAsync(p => p.Name == "Laptop");
    if (product != null)
    {
        product.Price = 70000;
        await context.SaveChangesAsync();
        Console.WriteLine($"Updated Laptop price to Rs.{product.Price}");
    }

    // Delete — Remove marks entity for deletion; SaveChangesAsync generates DELETE SQL
    var toDelete = await context.Products.FirstOrDefaultAsync(p => p.Name == "Rice Bag");
    if (toDelete != null)
    {
        context.Products.Remove(toDelete);
        await context.SaveChangesAsync();
        Console.WriteLine("Deleted Rice Bag.\n");
    }
}

// ============================================================
// LAB 7: LINQ Queries — Where, OrderBy, Select, DTOs
// Objective: Filter, sort, and project data
// ============================================================
static async Task Lab7_LinqQueries()
{
    Console.WriteLine("--- Lab 7: LINQ Queries ---");
    using var context = new AppDbContext();

    // Filter and sort
    var filtered = await context.Products
        .Where(p => p.Price > 1000)
        .OrderByDescending(p => p.Price)
        .ToListAsync();

    Console.WriteLine("Products > Rs.1000 (sorted by price desc):");
    foreach (var p in filtered)
        Console.WriteLine($"  {p.Name} - Rs.{p.Price}");

    // Project into anonymous type
    var anon = await context.Products
        .Select(p => new { p.Name, p.Price })
        .ToListAsync();
    Console.WriteLine("\nProjected (Name, Price):");
    foreach (var p in anon)
        Console.WriteLine($"  {p.Name}: Rs.{p.Price}");

    // Lab 12: Project into DTO — breaks circular reference
    var dtos = await context.Products
        .Select(p => new ProductDTO
        {
            Name         = p.Name,
            Price        = p.Price,
            CategoryName = p.Category.Name
        })
        .ToListAsync();

    Console.WriteLine("\nProductDTOs:");
    foreach (var d in dtos)
        Console.WriteLine($"  {d.Name} | Rs.{d.Price} | {d.CategoryName}");
    Console.WriteLine();
}

// ============================================================
// LAB 10: Eager, Explicit, and Lazy Loading
// Objective: Load related data using different strategies
// ============================================================
static async Task Lab10_Loading()
{
    Console.WriteLine("--- Lab 10: Loading Strategies ---");
    using var context = new AppDbContext();

    // Eager Loading — Include fetches Category in the same SQL query (JOIN)
    var withCategory = await context.Products
        .Include(p => p.Category)
        .ToListAsync();
    Console.WriteLine("Eager Loading:");
    foreach (var p in withCategory)
        Console.WriteLine($"  {p.Name} — Category: {p.Category?.Name}");

    // Explicit Loading — load related entity on demand after initial fetch
    var product = await context.Products.FirstAsync();
    await context.Entry(product).Reference(p => p.Category).LoadAsync();
    Console.WriteLine($"\nExplicit Loading — {product.Name} Category: {product.Category?.Name}");

    // Lazy Loading — navigation properties load automatically when accessed
    // (enabled by UseLazyLoadingProxies() in AppDbContext + virtual keyword)
    // NOTE: Lazy loading fires a separate SQL query per navigation access — use with care
    var lazyProduct = await context.Products.FirstAsync();
    Console.WriteLine($"\nLazy Loading — {lazyProduct.Name} Category: {lazyProduct.Category?.Name}");
    Console.WriteLine();
}

// ============================================================
// LAB 11: One-to-One and Many-to-Many Relationships
// Objective: Insert and query related entities
// ============================================================
static async Task Lab11_Relationships()
{
    Console.WriteLine("--- Lab 11: Relationships ---");
    using var context = new AppDbContext();

    // One-to-One: Add ProductDetail linked to a Product
    var product = await context.Products.FirstOrDefaultAsync();
    if (product != null && product.ProductDetail == null)
    {
        var detail = new ProductDetail { ProductId = product.Id, WarrantyInfo = "2 Years Warranty" };
        await context.ProductDetails.AddAsync(detail);
        await context.SaveChangesAsync();
        Console.WriteLine($"One-to-One: Added warranty info for {product.Name}");
    }

    // Many-to-Many: Create tags and link to products
    var tag1 = new Tag { Name = "On Sale" };
    var tag2 = new Tag { Name = "New Arrival" };
    await context.Tags.AddRangeAsync(tag1, tag2);

    var laptop = await context.Products.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Name == "Laptop");
    if (laptop != null)
    {
        laptop.Tags.Add(tag1);
        laptop.Tags.Add(tag2);
        await context.SaveChangesAsync();
        Console.WriteLine($"Many-to-Many: Tagged {laptop.Name} with '{tag1.Name}' and '{tag2.Name}'");
    }
    Console.WriteLine();
}

// ============================================================
// LAB 13: AsNoTracking and Compiled Queries
// Objective: Optimise read-only queries
// ============================================================
static async Task Lab13_Tracking()
{
    Console.WriteLine("--- Lab 13: Query Tracking ---");
    using var context = new AppDbContext();

    // AsNoTracking — EF does not track returned entities in the change tracker
    // Best for read-only scenarios (reports, dashboards) — faster and less memory
    var readOnly = await context.Products
        .AsNoTracking()
        .ToListAsync();
    Console.WriteLine($"AsNoTracking — loaded {readOnly.Count} products (not tracked)");

    // Compiled Query — pre-compiles LINQ expression to avoid repeated translation overhead
    // Best for frequently executed queries in hot paths
    var compiled = EF.CompileAsyncQuery(
        (AppDbContext ctx, decimal minPrice) =>
            ctx.Products.Where(p => p.Price > minPrice)
    );

    var result = new List<Product>();
    await foreach (var p in compiled(context, 10000))
        result.Add(p);

    Console.WriteLine($"Compiled Query — products above Rs.10000: {result.Count}");
    Console.WriteLine();
}

// ============================================================
// LAB 14: Bulk Operations with EFCore.BulkExtensions
// Objective: High-performance batch updates for large datasets
// ============================================================
static async Task Lab14_BulkOperations()
{
    Console.WriteLine("--- Lab 14: Bulk Operations ---");
    using var context = new AppDbContext();

    var products = await context.Products.ToListAsync();

    // Simulate stock audit — increase StockQuantity by 10 for all products
    foreach (var p in products)
        p.StockQuantity += 10;

    // BulkUpdateAsync — single batched UPDATE instead of N individual UPDATEs
    // Far more performant than SaveChangesAsync() for 100+ records
    await context.BulkUpdateAsync(products);
    Console.WriteLine($"Bulk updated {products.Count} products (StockQuantity +10).");

    // Compare: regular SaveChangesAsync would generate one UPDATE per product
    Console.WriteLine("Regular SaveChangesAsync: 1 UPDATE per record = N round trips");
    Console.WriteLine("BulkUpdateAsync: 1 batch operation = 1 round trip\n");
}

// ============================================================
// LAB 15: Concurrency Conflict with RowVersion
// Objective: Detect and handle optimistic concurrency conflicts
// ============================================================
static async Task Lab15_Concurrency()
{
    Console.WriteLine("--- Lab 15: Concurrency ---");

    // Simulate two users fetching the same product simultaneously
    using var context1 = new AppDbContext();
    using var context2 = new AppDbContext();

    var product1 = await context1.Products.FirstOrDefaultAsync();
    var product2 = await context2.Products.FirstOrDefaultAsync(p => p.Id == product1!.Id);

    if (product1 == null || product2 == null)
    {
        Console.WriteLine("No products found for concurrency test.\n");
        return;
    }

    // User 1 updates price
    product1.Price = 68000;
    await context1.SaveChangesAsync();
    Console.WriteLine("User 1 saved price: Rs.68000");

    // User 2 also tries to update — RowVersion now mismatches
    product2.Price = 65000;

    try
    {
        // EF includes RowVersion in WHERE clause of UPDATE
        // If row was already updated, no rows affected → DbUpdateConcurrencyException
        await context2.SaveChangesAsync();
        Console.WriteLine("User 2 saved price: Rs.65000");
    }
    catch (DbUpdateConcurrencyException)
    {
        Console.WriteLine("Concurrency conflict detected! User 2's update was rejected.");
        Console.WriteLine("Resolution: reload the entity and re-apply changes.\n");
    }
}

// ============================================================
// MAIN — Run all labs in sequence
// ============================================================
try
{
    await Lab4_InsertData();
    await Lab5_RetrieveData();
    await Lab6_UpdateDelete();
    await Lab7_LinqQueries();
    await Lab10_Loading();
    await Lab11_Relationships();
    await Lab13_Tracking();
    await Lab14_BulkOperations();
    await Lab15_Concurrency();

    Console.WriteLine("=== All Labs Completed ===");
}
catch (Exception ex)
{
    Console.WriteLine($"\n[ERROR] Could not connect to SQL Server.");
    Console.WriteLine($"Details: {ex.Message}");
    Console.WriteLine("\nTo run this project:");
    Console.WriteLine("1. Ensure SQL Server is running");
    Console.WriteLine("2. Update connection string in Data/AppDbContext.cs");
    Console.WriteLine("3. Run: dotnet ef database update");
    Console.WriteLine("4. Run: dotnet run");
}
