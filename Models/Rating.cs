namespace ShopTaskBD
{
    public class Rating
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public int Count { get; set; }
        public ICollection<Product>? Products { get; set; }

        // Конструктор
        public Rating(int id, decimal rate, int count)
        {
            Id = id;
            Rate = rate;
            Count = count;
        }
    }
}
