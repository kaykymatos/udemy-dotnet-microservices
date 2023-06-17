using GeekShopping.Web.Models;
using GeekShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly ICuponService _cuponService;

        public CartController(ICartService cartService, IProductService productService, ICuponService cuponService)
        {
            _cartService = cartService;
            _productService = productService;
            _cuponService = cuponService;
        }
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            CartViewModel cart = await FindUserCart();
            return View(cart);
        }
        [Authorize]
        [HttpPost]
        [ActionName("ApplyCupon")]
        public async Task<IActionResult> ApplyCupon(CartViewModel model)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            string userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            bool response = await _cartService.ApplyCoupon(model, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("RemoveCupon")]
        public async Task<IActionResult> RemoveCupon()
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            string userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;

            bool response = await _cartService.RemoveCoupon(userId, token);

            if (response)
            {
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
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
                if (!string.IsNullOrEmpty(response.CartHeader.CuponCode))
                {
                    CuponViewModel cupon = await _cuponService.GetCupon(response.CartHeader.CuponCode, token);
                    if (cupon?.CuponCode != null)
                    {
                        response.CartHeader.DiscountAmount = cupon.DiscountAmount;
                    }
                }
                foreach (CartDetailViewModel detail in response.CartDetails)
                {
                    response.CartHeader.PurchaseAmount += (detail.Product.Price * detail.Count);
                }
                response.CartHeader.PurchaseAmount -= response.CartHeader.DiscountAmount;
            }
            return response;
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            return View(await FindUserCart());
        }
    }
}
