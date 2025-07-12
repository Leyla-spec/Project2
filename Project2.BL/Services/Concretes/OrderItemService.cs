using Project2.BL.Exceptions;
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
                throw new ArgumentNullException(nameof(orderItem), "Order item null ola bilməz");
            if (_context.OrderItem.Any(o => o.MenuItemId == orderItem.MenuItemId && o.Count == orderItem.Count))
                throw new DuplicateMenuItemException("Eyni menu item və saylı order item artıq mövcuddur");
            _context.OrderItem.Add(orderItem);
            _context.SaveChanges();
        }

        public void DeleteOrderItem(int? orderItemId)
        {
            if (orderItemId is null || orderItemId <= 0)
                throw new ArgumentException("Order item id-si 0-dan böyük olmnalıdır.");
            var orderItem = GetOrderItemById(orderItemId);
            if (orderItem == null)
                throw new OrderNotFoundException($"Bu id ilə uyğun order item tapılmadı");
            _context.OrderItem.Remove(orderItem);
            _context.SaveChanges();
        }

        public List<OrderItem> GetAllOrderItems()
        {
            var orderItems = _context.OrderItem.ToList();
            if (orderItems == null || !orderItems.Any())
                throw new OrderNotFoundException("Order item tapılmadı.");
            return orderItems;
        }

        public OrderItem GetOrderItemById(int? orderItemId)
        {
            if (orderItemId is null)
                throw new ArgumentNullException("Order item id-si 0-dan böyük olmnalıdır.", nameof(orderItemId));
            var orderItem = _context.OrderItem.SingleOrDefault(o => o.Id == orderItemId);
            if (orderItem == null)
                throw new OrderNotFoundException($"Id {orderItemId} ilə uyğun order item tapılmadı.");
            return orderItem;
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem), "Order item null ola bilməz.");
            var existingOrderItem = GetOrderItemById(orderItem.Id);
            if (existingOrderItem == null)
                throw new OrderNotFoundException($"Id {orderItem.Id} ilə uyğun order item tapılmadı..");
            existingOrderItem.MenuItem = orderItem.MenuItem;
            existingOrderItem.Count = orderItem.Count;
            _context.SaveChanges();
        }
    }
}