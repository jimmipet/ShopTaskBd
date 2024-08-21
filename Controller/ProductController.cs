using Microsoft.AspNetCore.Mvc;
using ShopTaskBD;

[ApiController]
[Route("/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        if (products == null)
        {
            return NotFound();
        }
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost("{id}/addtocart")]
    public async Task<IActionResult> AddToCart(int id)
    {
        var result = await _productService.AddProductToCartAsync(id);
        if (!result)
        {
            return BadRequest("Не удалось добавить товар в корзину.");
        }
        return Ok(result);
    }

    [HttpGet("cart")]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCartItems()
    {
        var cartItems = await _productService.GetCartItemsAsync();
        return Ok(cartItems);
    }

    [HttpDelete("{id}/removefromcart")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var result = await _productService.RemoveProductFromCartAsync(id);
        return Ok(result);
    }


    [HttpPut("{id}/increasequantity")]
    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        var result = await _productService.IncreaseProductQuantityAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}/decreasequantity")]
    public async Task<IActionResult> DecreaseQuantity(int id)
    {
        var result = await _productService.DecreaseProductQuantityAsync(id);
        return Ok(result);
    }

    [HttpPost("add-new-product")]

    public async Task<IActionResult> CreateProduct([FromBody] CardItemDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Invalid product data.");
        }

        var newProductId = await _productService.CreateProductAsync(productDto);
        return Ok(newProductId);
    }

    [HttpPut("{id}/update")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] CardItemDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Invalid product data.");
        }

        var result = await _productService.UpdateProductAsync(id, productDto);
        if (!result)
        {
            return NotFound("Product not found.");
        }

        return Ok(result);
    }
}