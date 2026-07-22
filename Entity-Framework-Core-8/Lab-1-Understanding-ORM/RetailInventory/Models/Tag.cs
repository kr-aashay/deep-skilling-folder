namespace RetailInventory.Models;

// Lab 11: Tag model — many-to-many with Product
// EF Core 8 handles the join table automatically (no explicit join entity needed)
public class Tag
{
    public int    Id       { get; set; }
    public string Name     { get; set; } = string.Empty;

    // Navigation property — a Tag can apply to many Products
    public List<Product>  Products   { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}
