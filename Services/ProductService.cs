using System.Text.Json;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;

    public ProductService(HttpClient httpClient, ApplicationDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task FetchAndSaveProductsAsync()
    {
        if (_context.Products.Any())
        {
            return;
        }

        var response = await _httpClient.GetAsync("https://fakestoreapi.com/products");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (products != null)
        {
            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();
        }
    }
}