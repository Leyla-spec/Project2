using Microsoft.EntityFrameworkCore;
using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;

namespace Project2.BL.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly MenuAndOrderDbContext _context;
        public OrderService()
        {
            _context = new MenuAndOrderDbContext();
        }
        public void AddOrder(Orders order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

            _context.Orders.Add(order);
            _context.SaveChanges();
        }


        public void DeleteOrder(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Order ID cannot be null.");

            var order = GetOrderById(id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID not found.");

            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public List<Orders> GetAllOrders()
        {
            return _context.Orders
                           .Include(o => o.OrderItem)
                           .ThenInclude(oi => oi.MenuItem)
                           .ToList();  
        }

        public List<Orders> GetOrderByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Orders GetOrderById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Order ID cannot be null.");

            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");

            return order;
        }

        public List<Orders> GetOrdersByDatesInterval(DateTime start, DateTime end)
        {
            return _context.Orders
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToList();
        }

        public List<Orders> GetOrdersByPriceInterval(decimal startPrice, decimal endPrice)
        {
            return _context.Orders
                .Where(o => o.TotalPrice >= startPrice && o.TotalPrice <= endPrice)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToList();
        }

        public void UpdateOrder(Orders order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

            var existingOrder = GetOrderById(order.Id);
            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {order.Id} not found.");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.OrderItem = order.OrderItem;
            _context.SaveChanges();
        }
    }
}