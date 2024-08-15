using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _context;

    public ProductsController(ProductService productService, ApplicationDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    [HttpPost("save")]
    public async Task<IActionResult> FetchAndSaveProducts()
    {
        await _productService.FetchAndSaveProductsAsync();
        return Ok("Данные были добавлены в базу данных");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
            var products = await (from p in _context.Products
                          orderby p.id 
                          select p).ToListAsync();
        if (products == null)
        {
            return NotFound();
        }
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await (from p in _context.Products
                             where p.id == id
                             select p).FirstOrDefaultAsync();
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }
}