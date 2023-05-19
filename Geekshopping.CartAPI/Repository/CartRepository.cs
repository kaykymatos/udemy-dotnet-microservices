using AutoMapper;
using Geekshopping.CartAPI.Data.ValueObjects;
using Geekshopping.CartAPI.Model;
using Geekshopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Geekshopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySqlContext _context;
        private readonly IMapper _mapper;

        public CartRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> ApplyCupon(string userId, string cuponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long icartDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);
            var product = await _context.Product.FirstOrDefaultAsync(
                x =>
                x.Id == vo.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            {
                _context.Product.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }
            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                x =>
                x.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _context.CartDetail.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else
            {
                var carDetail = await _context.CartDetail.AsNoTracking().FirstOrDefaultAsync(
                    x =>
                    x.ProductId == vo.CartDetails.FirstOrDefault().ProductId &&
                    x.CartHeaderId == cartHeader.Id);

                if (carDetail == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetail.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += carDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = carDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = carDetail.CartHeaderId;
                    _context.CartDetail.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);
        }
    }
}
