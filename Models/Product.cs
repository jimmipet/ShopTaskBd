public class Product
{
    public int id { get; set; }
    public string? title { get; set; }
    public decimal price { get; set; }
    public string? description { get; set; }
    public string? category { get; set; }
    public string? image { get; set; }
    public Rating? rating { get; set; }
}

public class Rating
{
    public int id { get; set; }
    public decimal rate { get; set; }
    public int count { get; set; }
}