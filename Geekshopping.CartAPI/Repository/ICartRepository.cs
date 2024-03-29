﻿using Geekshopping.CartApi.Data.ValueObjects;

namespace Geekshopping.CartApi.Repository
{
    public interface ICartRepository
    {
        Task<CartVO> FindCartByUserId(string userId);
        Task<CartVO> SaveOrUpdateCart(CartVO cart);
        Task<bool> RemoveFromCart(long icartDetailsId);
        Task<bool> ApplyCupon(string userId, string cuponCode);
        Task<bool> RemoveCupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}
