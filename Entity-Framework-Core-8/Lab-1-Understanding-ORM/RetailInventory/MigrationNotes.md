# Lab 3: EF Core CLI Migrations

## Install EF Core CLI (run once globally)
```bash
dotnet tool install --global dotnet-ef
```

## Lab 3 — Create Initial Migration
```bash
dotnet ef migrations add InitialCreate
```
- Generates a `Migrations/` folder with:
  - `<timestamp>_InitialCreate.cs` — Up() creates tables, Down() drops them
  - `<timestamp>_InitialCreate.Designer.cs` — snapshot metadata
  - `AppDbContextModelSnapshot.cs` — current model state

## Lab 3 — Apply Migration (creates the database + tables)
```bash
dotnet ef database update
```
- Connects to SQL Server using the connection string in AppDbContext
- Creates `RetailInventoryDB` with tables: Products, Categories, ProductDetails, Tags
- Verify in SSMS: both tables exist with correct columns and foreign keys

---

## Lab 8 — Add StockQuantity Column
After adding `public int StockQuantity { get; set; }` to Product model:
```bash
dotnet ef migrations add AddStockQuantity
dotnet ef database update
```
- Adds `StockQuantity` column to Products table
- Verify in SSMS: new column visible in Products table

---

## Lab 9 — Seed Initial Data
After adding `HasData()` in `OnModelCreating`:
```bash
dotnet ef migrations add SeedInitialData
dotnet ef database update
```
- Inserts seed rows into Categories and Products on first apply

---

## Lab 11 — Relationships Migration
After adding ProductDetail, Tag models and configuring relationships:
```bash
dotnet ef migrations add AddRelationships
dotnet ef database update
```

---

## Rollback a Migration
```bash
dotnet ef database update <PreviousMigrationName>
dotnet ef migrations remove
```

## List All Migrations
```bash
dotnet ef migrations list
```
