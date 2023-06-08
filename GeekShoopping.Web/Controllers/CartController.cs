using GeekShoopping.Web.Models;
using GeekShoopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShoopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            CartViewModel cart = await FindUserCart();
            return View(cart);
        }

        public async Task<IActionResult> Remove(int id)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            string userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            bool response = await _cartService.RemoveFromCart(id, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        private async Task<CartViewModel> FindUserCart()
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            string userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            CartViewModel response = await _cartService.FindCartByUserId(userId, token);

            if (response?.CartHeader != null)
            {
                foreach (CartDetailViewModel detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }
            }
            return response;
        }
    }
}
