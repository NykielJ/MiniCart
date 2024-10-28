namespace MinicartAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Pattern { get; set; }

    }

}
