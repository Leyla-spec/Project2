<<<<<<< HEAD
﻿using Project2.Core.Entities;

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
=======
﻿using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderItemsInterface
    {
        public List<OrderItem> GetAllOrderItems();
        public OrderItem GetOrderItemById(int id);
        public void AddOrderItem(OrderItem orderItem);
        public void UpdateOrderItem(OrderItem orderItem);
        public void DeleteOrderItem(int id);
    }
}
>>>>>>> 704c33f55dcbb2ed9b5e5db5e52a593e8ce259ef
