using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;

namespace Project2.BL.Services.Concretes
{
    public class OrderItemService : IOrderItemService
    {
        private readonly MenuAndOrderDbContext _context;
        public OrderItemService()
        {
            _context = new MenuAndOrderDbContext();
        }
        public void AddOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem), "Order item cannot be null.");
            if (_context.OrderItem.Any(o => o.MenuItem.Id == orderItem.MenuItem.Id && o.Count == orderItem.Count))
                throw new ArgumentException("Order item with the same menu item and count already exists.");
            _context.OrderItem.Add(orderItem);
            _context.SaveChanges();
        }

        public void DeleteOrderItem(int? orderItemId)
        {
            if (orderItemId is null || orderItemId <= 0)
                throw new ArgumentException("Order item ID must be greater than zero.");
            var orderItem = GetOrderItemById(orderItemId);
            if (orderItem == null)
                throw new KeyNotFoundException($"Order item with ID not found.");
            _context.OrderItem.Remove(orderItem);
            _context.SaveChanges();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            var orderItems = _context.OrderItem.ToList();
            if (orderItems == null || !orderItems.Any())
                throw new KeyNotFoundException("No order items found.");
            return orderItems;
        }

        public OrderItem GetOrderItemById(int? orderItemId)
        {
            if (orderItemId is null)
                throw new ArgumentException("Order item ID must be greater than zero.", nameof(orderItemId));
            var orderItem = _context.OrderItem.SingleOrDefault(o => o.Id == orderItemId);
            if (orderItem == null)
                throw new KeyNotFoundException($"Order item with ID {orderItemId} not found.");
            return orderItem;
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem), "Order item cannot be null.");
            var existingOrderItem = GetOrderItemById(orderItem.Id);
            if (existingOrderItem == null)
                throw new KeyNotFoundException($"Order item with ID {orderItem.Id} not found.");
            existingOrderItem.MenuItem = orderItem.MenuItem;
            existingOrderItem.Count = orderItem.Count;
            _context.SaveChanges();
        }
    }
}