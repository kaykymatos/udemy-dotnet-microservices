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
            CartHeader cartHeader = await _context.CartHeaders
                         .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cartHeader != null)
            {


                _context.CartDetail.RemoveRange(_context.CartDetail.Where(x => x.CartHeaderId == cartHeader.Id));
                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId)
            };
            cart.CartDetails = _context.CartDetail.Where(x => x.CartHeaderId == cart.CartHeader.Id)
                .Include(x => x.Product);
            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveCupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetail
                    .FirstOrDefaultAsync(x => x.Id == cartDetailsId);

                int total = _context.CartDetail
                    .Where(x => x.CartHeaderId == cartDetail.CartHeaderId).Count();

                _context.CartDetail.Remove(cartDetail);
                if (total == 1)
                {
                    CartHeader cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(x => x.Id == cartDetail.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);

                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = _mapper.Map<Cart>(vo);
            Product product = await _context.Product.FirstOrDefaultAsync(
                x =>
                x.Id == vo.CartDetails.FirstOrDefault().ProductId);
            if (product == null)
            {
                _context.Product.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }
            CartHeader cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
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
                CartDetail carDetail = await _context.CartDetail.AsNoTracking().FirstOrDefaultAsync(
                    x =>
                    x.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    x.CartHeaderId == cartHeader.Id);

                if (carDetail == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
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
