using System;
using Microsoft.EntityFrameworkCore;
using TradingManagement.Data;
using TradingManagement.Entity;

namespace TradingManagement.Services
{
    public class OrderService
    {
        private readonly TradingDbContext _context;

        public OrderService(TradingDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order model)
        {
            _context.Orders.Add(model);
            _context.SaveChanges();
        }

        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            var orders = await _context.Orders.Where(t => t.userId == userId).ToListAsync();
            if (orders != null)
            {
                return orders;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Order>> GetFinishedOrdersByUserId(int userId)
        {
            var orders = await _context.Orders.Where(t => t.userId == userId && t.finished == true).ToListAsync();
            if (orders != null)
            {
                return orders;
            }
            else
            {
                return null;
            }
        }

        public void OpenOrderByIdAndUserId(int id, int userId, bool isOpen)
        {
            var order = _context.Orders.FirstOrDefault(o => o.id == id && o.userId == userId);

            if (order != null)
            {
                order.isOpen = isOpen;
                _context.SaveChanges();
            }
        }

        public void CloseOrderByIdAndUserId(int id, int userId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.id == id && o.userId == userId);

            if (order != null && order.isOpen == true)
            {
                order.finished = true;
                _context.SaveChanges();
            }
        }

    }
}

