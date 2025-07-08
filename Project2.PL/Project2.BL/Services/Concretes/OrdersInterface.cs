using Microsoft.EntityFrameworkCore;
using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;

namespace Project2.BL.Services.Concretes
{
    public class OrdersInterface : IOrderInterface
    {
        private readonly MenuandOrderDBContext _context;
        public OrdersInterface()
        {
            _context = new MenuandOrderDBContext();
        }
        public void AddOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("Order cannot be null.");
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem);
            _context.Orders.Add(order);
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

        public List<Order> GetAllOrders()
        {
            var orders = _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .ToList();
            if (orders == null || !orders.Any())
                throw new KeyNotFoundException("No orders found.");
            return orders;
        }

        public List<Order> GetOrderByDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Order ID cannot be null.");
            
            var order = _context.Orders.SingleOrDefault(o=> o.Id== id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            
            return order;
        }

        public List<Order> GetOrdersByDatesInterval(Order dateTime)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByPriceInterval(Order price)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

            var existingOrder = GetOrderById(order.Id);
            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {order.Id} not found.");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.OrderItems = order.OrderItems;
            _context.SaveChanges();
        }
    }
}
