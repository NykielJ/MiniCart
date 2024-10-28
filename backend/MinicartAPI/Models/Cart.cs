namespace MinicartAPI.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal ShippingCost { get; set; } = 15.00m; // Koszty dostaw zakdładam że 15 $
        public decimal Total => Items.Sum(item => item.Price * item.Quantity) + ShippingCost;
    }

}