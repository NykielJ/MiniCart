namespace MinicartAPI.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal ShippingCost => CalculateShippingCost();
        public decimal Total => Items.Sum(item => item.Price * item.Quantity) + ShippingCost;

        private decimal CalculateShippingCost()
        {
            decimal itemsTotal = Items.Sum(item => item.Price * item.Quantity);
            return itemsTotal > 100 ? 0m : 15m;
        }
    }
}