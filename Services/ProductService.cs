using Microsoft.EntityFrameworkCore;
using ShopTaskBD;

public class ProductService
{
    private readonly ApplicationDbContext _context;
    private readonly CartService _cartService;

    public ProductService(ApplicationDbContext context, CartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }
    //все товары
    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products
            .OrderBy(p => p.Id)
            .ToListAsync();
    }
    //товар по id
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    //добавление товаров в корзину
    public async Task<bool> AddProductToCartAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return false;
        }

        var cartItem = await _context.Carts.FirstOrDefaultAsync(ci => ci.Id == productId);
        if (cartItem != null)
        {
            _cartService.IncreaseQuantity(cartItem);
        }
        else
        {
            cartItem = new Cart(product.Id, product.Title, product.Price, product.Description, product.Category, product.Image)
            {
                Count = 1
            };
            _context.Carts.Add(cartItem);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    //получение товаров корзины
    public async Task<List<Cart>> GetCartItemsAsync()
    {
        return await _context.Carts
            .OrderBy(ci => ci.Id)
            .ToListAsync();
    }
    //удаление товара
    public async Task<bool> RemoveProductFromCartAsync(int productId)
    {
        var cartItem = await _context.Carts.FirstOrDefaultAsync(ci => ci.Id == productId);
        if (cartItem == null)
        {
            return false;
        }

        _context.Carts.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    // Увеличение количества товара в корзине
    public async Task<bool> IncreaseProductQuantityAsync(int productId)
    {
        var cartItem = await _context.Carts.FirstOrDefaultAsync(ci => ci.Id == productId);
        if (cartItem == null)
        {
            return false;
        }

        _cartService.IncreaseQuantity(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    // Уменьшение количества товара в корзине
    public async Task<bool> DecreaseProductQuantityAsync(int productId)
    {
        var cartItem = await _context.Carts.FirstOrDefaultAsync(ci => ci.Id == productId);
        Console.WriteLine(cartItem);
        if (cartItem == null || cartItem.Count <= 1)
        {
            return false;
        }

        _cartService.DecreaseQuantity(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> CreateProductAsync(CardItemDto productDto)
    {
        var product = CreateProduct(productDto);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        product.RatingId = product.Id;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return product.Id;
    }
    private Product CreateProduct(CardItemDto productDto)
    {
        return new Product(
            id: 0,
            title: productDto.Title,
            price: productDto.Price,
            description: productDto.Description,
            category: productDto.Category,
            image: productDto.Image,
            ratingId: null
        );
    }
    public async Task<int?> UpdateProductAsync(int id, CardItemDto productDto)
    {
        var updateProduct = await _context.Products.FindAsync(id);
        if (updateProduct == null)
        {
            return null;
        }

        updateProduct.Title = productDto.Title;
        updateProduct.Price = productDto.Price;
        updateProduct.Description = productDto.Description;
        updateProduct.Category = productDto.Category;
        updateProduct.Image = productDto.Image;

        _context.Products.Update(updateProduct);
        await _context.SaveChangesAsync();
        return updateProduct.Id; ;
    }
}