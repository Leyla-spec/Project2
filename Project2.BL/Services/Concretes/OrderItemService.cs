using Project2.BL.Exceptions;
using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;
using Project2.DAL.Repositories.Conceretes;

namespace Project2.BL.Services.Concretes
{
    public class OrderItemService : IOrderItemService 
    {
        private readonly Repositories<OrderItem> _repository;
        public OrderItemService(MenuAndOrderDbContext context)
        {
            _repository = new Repositories<OrderItem>(context);
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem), "Order item null ola bilməz");

            var allItems = await _repository.GetAllAsync();
            if (allItems.Any(o => o.MenuItemId == orderItem.MenuItemId && o.Count == orderItem.Count))
                throw new DuplicateMenuItemException("Eyni menu item və saylı order item artıq mövcuddur");

            await _repository.AddAsync(orderItem);
        }

        public async Task DeleteOrderItemAsync(int? orderItemId)
        {
            if (orderItemId is null || orderItemId <= 0)
                throw new ArgumentException("Order item id-si 0-dan böyük olmalıdır.");

            var orderItem = await _repository.GetByIdAsync(orderItemId.Value);
            if (orderItem == null)
                throw new OrderNotFoundException($"Bu id ilə uyğun order item tapılmadı");

            await _repository.DeleteAsync(orderItemId);
        }

        public async Task<List<OrderItem>> GetAllOrderItemsAsync()
        {
            var orderItems = await _repository.GetAllAsync();
            if (orderItems == null || !orderItems.Any())
                throw new OrderNotFoundException("Order item tapılmadı.");
            return orderItems;
        }

        public async Task<OrderItem?> GetOrderItemByIdAsync(int? orderItemId, bool isTracking = false)
        {

            if (orderItemId is null)
                throw new ArgumentNullException("Order item id-si null ola bilməz", nameof(orderItemId));

            var orderItem = await _repository.GetByIdAsync(orderItemId.Value);
            if (orderItem == null)
                throw new OrderNotFoundException($"Id {orderItemId} ilə uyğun order item tapılmadı.");
            return orderItem;
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException(nameof(orderItem), "Order item null ola bilməz.");

            var existingOrderItem = await _repository.GetByIdAsync(orderItem.Id);
            if (existingOrderItem == null)
                throw new OrderNotFoundException($"Id {orderItem.Id} ilə uyğun order item tapılmadı.");

            await _repository.UpdateAsync(orderItem);
        }
    }

}