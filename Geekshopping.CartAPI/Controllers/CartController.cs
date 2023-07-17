using Geekshopping.CartApi.Data.ValueObjects;
using Geekshopping.CartApi.Repository;
using Geekshopping.CartApi.Messages;
using Geekshopping.CartApi.RabbitMQSender;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Geekshopping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICuponRepository _cuponRepository;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(
            ICartRepository cartRepository,
            ICuponRepository cuponRepository,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartRepository = cartRepository ??
                throw new ArgumentException(nameof(cartRepository));
            _cuponRepository = cuponRepository ??
                throw new ArgumentException(nameof(cuponRepository));
            _rabbitMQMessageSender = rabbitMQMessageSender ??
                throw new ArgumentException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            CartVO cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            CartVO cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            CartVO cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            bool status = await _cartRepository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            bool status = await _cartRepository.ApplyCupon(vo.CartHeader.UserId, vo.CartHeader.CuponCode);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(string userId)
        {
            bool status = await _cartRepository.RemoveCupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            string token = await HttpContext.GetTokenAsync("access_token");
            if (vo?.UserId == null) return BadRequest();
            CartVO cart = await _cartRepository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();
            if (!string.IsNullOrEmpty(vo.CuponCode))
            {
                CuponVO cupon = await _cuponRepository.GetCupon(vo.CuponCode, token);
                if (vo.DiscountAmount != cupon.DiscountAmount) return StatusCode(412);
            }


            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            _rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");

            await _cartRepository.ClearCart(vo.UserId);
            return Ok(vo);
        }
    }
}