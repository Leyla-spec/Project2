using Microsoft.EntityFrameworkCore;
using Project2.BL.Exceptions;
using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;
using Project2.DAL.Repositories.Conceretes;

namespace Project2.BL.Services.Concretes
{
    public class OrderService : IOrderService
    { 
        private readonly Repositories<Orders> _repository;

        private readonly MenuAndOrderDbContext _context;

        public OrderService(MenuAndOrderDbContext context)
        {
            _repository = new Repositories<Orders>(context);
            _context = new MenuAndOrderDbContext();
        }

        public async Task AddAsync(Orders order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Sifariş null ola bilməz");

            await _repository.AddAsync(order);
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "ID null ola bilməz");

            var order = await _repository.GetByIdAsync(id.Value);
            if (order == null)
                throw new OrderNotFoundException($"{id} id-li sifariş tapılmadı.");

            await _repository.DeleteAsync(id);
        }

        public async Task<List<Orders>> GetAllAsync()
        {
            return await _repository.Table
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
        }

        public async Task<Orders?> GetByIdAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var order = await _repository.Table
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .SingleOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new OrderNotFoundException($"{id} id-li sifariş tapılmadı.");

            return order;
        }

        public async Task<List<Orders>> GetByPriceIntervalAsync(decimal startPrice, decimal endPrice)
        {
            if (startPrice < 0 || endPrice < 0)
                throw new InvalidPriceRangeException("Qiymətlər mənfi ola bilməz.");

            if (startPrice > endPrice)
                throw new InvalidPriceRangeException("Başlanğıc qiymət son qiymətdən böyük ola bilməz.");

            var orders = await _repository.Table
                .Where(o => o.TotalPrice >= startPrice && o.TotalPrice <= endPrice)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            if (!orders.Any())
                throw new OrderNotFoundException("Bu qiymət aralığında heç bir sifariş tapılmadı.");

            return orders;
        }

        public async Task UpdateAsync(Orders order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Sifariş null ola bilməz");

            var existingOrder = await _repository.GetByIdAsync(order.Id);
            if (existingOrder == null)
                throw new OrderNotFoundException($"{order.Id} id-li sifariş tapılmadı.");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalPrice = order.TotalPrice;
            existingOrder.OrderItem = order.OrderItem;

            await _repository.UpdateAsync(existingOrder);
        }

        public async Task<List<Orders>> GetOrderByDateAsync(DateTime dateTime)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate.Date == dateTime.Date)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            if (orders == null || orders.Count == 0)
                throw new OrderNotFoundException($"{dateTime:yyyy-MM-dd} tarixdə sifariş yoxdu");

            return orders;
        }

        public async Task<List<Orders>> GetOrdersByDatesIntervalAsync(DateTime start, DateTime end)
        {
            if (start > end)
                throw new ArgumentException("Başlanğıc tarix son tarixdən böyük ola bilməz.");

            var orders = await _context.Orders
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .Include(o => o.OrderItem)
                .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            if (orders.Count == 0)
                throw new OrderNotFoundException("Bu tarix aralığında sifariş tapılmadı.");

            return orders;
        }

    }
}