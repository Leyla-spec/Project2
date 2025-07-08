using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderItemsInterface
    {
        public List<OrderItem> GetAllOrderItems();
        public OrderItem GetOrderItemById(int? orderItemId);
        public void AddOrderItem(OrderItem orderItem);
        public void UpdateOrderItem(OrderItem orderItem);
        public void DeleteOrderItem(int? id);
    }
}
