namespace ShopTaskBD
{
    public class Cart
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public int Count { get; set; }

        // Конструктор
        public Cart(int id, string? title, decimal price, string? description, string? category, string? image)
        {
            Id = id;
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
        }
    }
}