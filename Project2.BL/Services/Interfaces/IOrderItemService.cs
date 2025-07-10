using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderItemService
    {
        void AddOrderItem(OrderItem orderItem);
        void DeleteOrderItem(int? orderItemId);
        List<OrderItem> GetAllOrderItems();
        OrderItem GetOrderItemById(int? orderItemId);
        void UpdateOrderItem(OrderItem orderItem);
    }
}
