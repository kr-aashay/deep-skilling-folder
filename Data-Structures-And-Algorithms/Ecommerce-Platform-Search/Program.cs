using EcommercePlatformSearch;

List<Product> products = new List<Product>
{
    new Product(1, "Laptop",        999.99),
    new Product(2, "Phone",         499.99),
    new Product(3, "Headphones",     79.99),
    new Product(4, "Keyboard",       49.99),
    new Product(5, "Gaming Laptop", 1499.99),
    new Product(6, "Mouse",          29.99),
};

// 1. Search by name
Console.WriteLine("=== Search by Name: 'laptop' ===");
var byName = SearchEngine.SearchByName(products, "laptop");
byName.ForEach(Console.WriteLine);

// 2. Search by price range
Console.WriteLine("\n=== Search by Price: $50 - $500 ===");
var byPrice = SearchEngine.SearchByPriceRange(products, 50, 500);
byPrice.ForEach(Console.WriteLine);

// 3. Binary search by ID
Console.WriteLine("\n=== Binary Search by ID: 4 ===");
var byId = SearchEngine.SearchById(products, 4);
Console.WriteLine(byId != null ? byId.ToString() : "Product not found.");
