namespace RetailInventory.DTOs;

// Lab 7: DTO for projecting query results — avoids circular reference issues
// Lab 12: Use DTOs for API responses to break circular references
public class ProductDTO
{
    public string  Name         { get; set; } = string.Empty;
    public decimal Price        { get; set; }
    public string  CategoryName { get; set; } = string.Empty;
}
