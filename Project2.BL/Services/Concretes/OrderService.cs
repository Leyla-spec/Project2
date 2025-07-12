using Microsoft.EntityFrameworkCore;
using Project2.BL.Exceptions;
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
                throw new ArgumentNullException(nameof(order), "Sifariş null ola bilməz");

            _context.Orders.Add(order);
            _context.SaveChanges();
        }


        public void DeleteOrder(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("Sifariş id-si null ola bilməz.");

            var order = GetOrderById(id);
            if (order == null)
                throw new OrderNotFoundException($"Bu id-də sifariş tapılmadı");

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
            var orders = _context.Orders
                .Where(o => o.OrderDate.Date == dateTime.Date)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToList();

            if (orders == null || orders.Count == 0)
                throw new OrderNotFoundException($"{dateTime:yyyy-MM-dd} tarixdə sifariş yoxdu");

            return orders;
        }

        public Orders GetOrderById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Sifariş id-si null ola bilməz");

            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
                throw new OrderNotFoundException($"{id} id-li sifariş tapılmadı.");

            return order;
        }

        public List<Orders> GetOrdersByDatesInterval(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("Başlanğıc tarix son tarixdən böyük ola bilməz.");

            var orders = _context.Orders
                                 .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                                 .Include(o => o.OrderItem)
                                 .ThenInclude(oi => oi.MenuItem)
                                 .ToList();

            if (orders.Count == 0)
                throw new OrderNotFoundException("Bu tarix aralığında sifariş tapılmadı.");

            return orders;
        }

        public List<Orders> GetOrdersByPriceInterval(decimal startPrice, decimal endPrice)
        {
            if (startPrice < 0 || endPrice < 0)
                throw new InvalidPriceRangeException("Qiymətlər mənfi ola bilməz.");

            if (startPrice > endPrice)
                throw new InvalidPriceRangeException("Başlanğıc qiymət son qiymətdən böyük ola bilməz.");

            var orders = _context.Orders
                .Where(o => o.TotalPrice >= startPrice && o.TotalPrice <= endPrice)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToList();

            if (orders.Count == 0)
                throw new OrderNotFoundException("Bu qiymət aralığında heç bir sifariş tapılmadı.");

            return orders;
        }

        public void UpdateOrder(Orders order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Sifariş null ola bilməz");

            var existingOrder = GetOrderById(order.Id);
            if (existingOrder == null)
                throw new OrderNotFoundException($"{order.Id} id-li sifariş tapılmadı.");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.OrderItem = order.OrderItem;
            _context.SaveChanges();
        }
    }
}