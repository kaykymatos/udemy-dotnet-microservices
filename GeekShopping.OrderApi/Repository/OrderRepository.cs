﻿using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MySqlContext> _context;
        public OrderRepository(DbContextOptions<MySqlContext> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrder(OrderHeader header)
        {
            if (header == null) return false;

            await using MySqlContext _db = new(_context);
            _db.Headers.Add(header);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            await using MySqlContext _db = new(_context);
            OrderHeader header = await _db.Headers.FirstOrDefaultAsync(x => x.Id == orderHeaderId);
            if (header != null)
            {
                header.PaymentStatus = status;
                await _db.SaveChangesAsync();
            }
        }
    }
}
