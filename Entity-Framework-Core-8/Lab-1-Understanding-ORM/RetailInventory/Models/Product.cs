using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RetailInventory.Models;

// Lab 2: Product model — maps to the Products table
public class Product
{
    public int     Id           { get; set; }
    public string  Name         { get; set; } = string.Empty;
    public decimal Price        { get; set; }

    // Lab 8: StockQuantity column added via migration AddStockQuantity
    public int     StockQuantity { get; set; }

    // Lab 15: RowVersion for optimistic concurrency control
    // [Timestamp] marks this as a concurrency token — EF includes it in UPDATE WHERE clause
    [Timestamp]
    public byte[]? RowVersion { get; set; }

    // Foreign key to Category
    public int      CategoryId { get; set; }

    // Lab 12: [JsonIgnore] prevents circular reference in serialization
    // Product → Category → Products → Product (infinite loop)
    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;

    // Lab 11: One-to-one — each Product has one ProductDetail
    public virtual ProductDetail? ProductDetail { get; set; }

    // Lab 11: Many-to-many — a Product can have many Tags
    public List<Tag> Tags { get; set; } = new();
}
