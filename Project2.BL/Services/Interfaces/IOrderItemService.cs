using Project2.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int? orderItemId);
        Task<List<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem?> GetOrderItemByIdAsync(int? orderItemId, bool isTracking = false);
        Task UpdateOrderItemAsync(OrderItem orderItem);
    }
}

