using Microsoft.AspNetCore.Mvc;
using MinicartAPI.Models;
using MinicartAPI.Services;
using System.Threading.Tasks;

namespace MinicartAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartAsync()
        {
            var cart = await _cartService.GetCartAsync();
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddToCartAsync([FromBody] CartItem newItem)
        {
            if (newItem == null || string.IsNullOrWhiteSpace(newItem.Name) || newItem.Price <= 0)
            {
                return BadRequest("Invalid item data.");
            }

            await _cartService.AddToCartAsync(newItem);
            return Ok(await _cartService.GetCartAsync());
        }

        [HttpDelete("remove/{itemId}")]
        public async Task<ActionResult> RemoveFromCartAsync(int itemId)
        {
            try
            {
                await _cartService.RemoveFromCartAsync(itemId);
                return Ok(await _cartService.GetCartAsync());
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Item not found in cart.");
            }
        }

        [HttpPatch("updateQuantity/{itemId}")]
        public async Task<ActionResult> UpdateQuantityItemAsync(int itemId, [FromBody] int newQuantity)
        {
            try
            {
                await _cartService.UpdateQuantityAsync(itemId, newQuantity);
                return Ok(await _cartService.GetCartAsync());
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Item not found in cart.");
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}