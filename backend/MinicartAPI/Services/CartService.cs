using MinicartAPI.Models;
using System.Text.Json;
using System.IO;

namespace MinicartAPI.Services
{
    public class CartService
    {
        private readonly string _cartFilePath = "Data\\cart.json";

        public async Task<Cart> GetCartAsync()
        {
            if (File.Exists(_cartFilePath))
            {
                var json = await File.ReadAllTextAsync(_cartFilePath);
                return JsonSerializer.Deserialize<Cart>(json) ?? new Cart();
            }
            return new Cart();
        }

        public async Task AddToCartAsync(CartItem newItem)
        {
            var cart = await GetCartAsync();
            var existingItem = cart.Items.FirstOrDefault(item => item.Id == newItem.Id);

            if (existingItem != null)
            {
                existingItem.Quantity += newItem.Quantity;
            }
            else
            {
                throw new KeyNotFoundException("Item not found in cart.");
            }

            await SaveCartAsync(cart);
        }

        public async Task RemoveFromCartAsync(int itemId)
        {
            var cart = await GetCartAsync();
            var itemToRemove = cart.Items.FirstOrDefault(item => item.Id == itemId);

            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                await SaveCartAsync(cart);
            }
            else
            {
                throw new KeyNotFoundException("Item not found in cart.");
            }
        }

        public async Task UpdateQuantityAsync(int itemId, int newQuantity)
        {
            var cart = await GetCartAsync();
            var item = cart.Items.FirstOrDefault(item => item.Id == itemId);

            if (item != null)
            {
                if (newQuantity > 0)
                {
                    item.Quantity = newQuantity;
                    await SaveCartAsync(cart);
                }
                else
                {
                    throw new ArgumentException("Quantity must be greater than zero.");
                }
            }
            else
            {
                throw new KeyNotFoundException("Item not found in cart.");
            }
        }

        public async Task SaveCartAsync(Cart cart)
        {
            try
            {
                var directory = Path.GetDirectoryName(_cartFilePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonSerializer.Serialize(cart, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_cartFilePath, json);
                Console.WriteLine($"Cart saved successfully to {_cartFilePath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error saving cart to {_cartFilePath}: {ex.Message}");
                throw new IOException("Could not save the cart.", ex);
            }
        }
    }
}
