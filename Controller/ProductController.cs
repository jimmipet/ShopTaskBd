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
            return NoContent();
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
        return NoContent();
    }

    [HttpGet("cart")]
    public async Task<ActionResult<IEnumerable<Cart>>> GetCartItems()
    {
        var cartItems = await _productService.GetCartItemsAsync();
        if (cartItems == null || !cartItems.Any())
        {
            return NoContent();
        }
        return Ok(cartItems);
    }

    [HttpDelete("{id}/removefromcart")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        var result = await _productService.RemoveProductFromCartAsync(id);
        if (!result)
        {
            return NotFound("Продукт не найден в корзине.");
        }
        return NoContent();
    }


    [HttpPut("{id}/increasequantity")]
    public async Task<IActionResult> IncreaseQuantity(int id)
    {
        var result = await _productService.IncreaseProductQuantityAsync(id);
        if (!result)
        {
            return NotFound("Продукт не найден.");
        }
        return NoContent();
    }

    [HttpPut("{id}/decreasequantity")]
    public async Task<IActionResult> DecreaseQuantity(int id)
    {
        await _productService.DecreaseProductQuantityAsync(id);
        return NoContent();
    }

    [HttpPost]

    public async Task<IActionResult> CreateProduct([FromBody] CardItemDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Неверные данные");
        }

        var newProductId = await _productService.CreateProductAsync(productDto);
        return Ok(newProductId);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] CardItemDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest("Неверные данные");
        }

        var updatedProductId = await _productService.UpdateProductAsync(id, productDto);
        if (updatedProductId == null)
        {
            return NotFound("Продукт не найден");
        }

        return Ok(updatedProductId);
    }

}