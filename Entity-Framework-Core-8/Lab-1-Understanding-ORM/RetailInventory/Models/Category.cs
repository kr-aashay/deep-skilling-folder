namespace RetailInventory.Models;

// Lab 2: Category model — maps to the Categories table in SQL Server
// ORM: this C# class becomes a table; each property becomes a column
public class Category
{
    public int    Id       { get; set; }
    public string Name     { get; set; } = string.Empty;

    // Lab 2: Navigation property — one Category has many Products
    public List<Product> Products { get; set; } = new();

    // Lab 11: Many-to-many — a Category can have many Tags
    public List<Tag> Tags { get; set; } = new();
}
