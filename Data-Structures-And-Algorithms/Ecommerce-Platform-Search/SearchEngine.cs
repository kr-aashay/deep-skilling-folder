namespace EcommercePlatformSearch;

public class SearchEngine
{
    // Linear search by name
    public static List<Product> SearchByName(List<Product> products, string keyword)
    {
        List<Product> results = new List<Product>();
        foreach (var product in products)
        {
            if (product.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                results.Add(product);
        }
        return results;
    }

    // Linear search by price range
    public static List<Product> SearchByPriceRange(List<Product> products, double min, double max)
    {
        List<Product> results = new List<Product>();
        foreach (var product in products)
        {
            if (product.Price >= min && product.Price <= max)
                results.Add(product);
        }
        return results;
    }

    // Binary search by ID (list must be sorted by ID)
    public static Product? SearchById(List<Product> products, int id)
    {
        int left = 0, right = products.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;

            if (products[mid].Id == id)
                return products[mid];
            else if (products[mid].Id < id)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return null;
    }
}
