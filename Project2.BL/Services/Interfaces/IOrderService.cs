using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderService
    {
        void AddOrder(Orders order);
        void DeleteOrder(int? id);
        List<Orders> GetAllOrders();
        List<Orders> GetOrderByDate(DateTime dateTime);
        Orders GetOrderById(int? id);
        void UpdateOrder(Orders order);
    }
}
