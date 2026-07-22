using System.Text.Json.Serialization;

namespace RetailInventory.Models;

// Lab 11: One-to-one relationship with Product
public class ProductDetail
{
    public int    ProductDetailId { get; set; }
    public string WarrantyInfo    { get; set; } = string.Empty;

    // Foreign key
    public int     ProductId { get; set; }

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
