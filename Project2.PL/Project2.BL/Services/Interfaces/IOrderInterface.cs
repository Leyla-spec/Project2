<<<<<<< HEAD
﻿using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderInterface
    {
        public List<Order> GetAllOrders();
        public Order GetOrderById(int? id);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int? id);
        public void AddOrder(Order order);
        public List<Order> GetOrdersByDatesInterval(Order dateTime);
        public List<Order> GetOrderByDate(DateTime dateTime);
        public List<Order> GetOrdersByPriceInterval(Order price);
    }
}
=======
﻿using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderInterface
    {
        public List<Order> GetAllOrders();
        public Order GetOrderById(int id);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int id);
        public void AddOrder(Order order);
        public List<Order> GetOrdersByDatesInterval(Order dateTime);
        public List<Order> GetOrderByDate(DateTime dateTime);
        public List<Order> GetOrdersByPriceInterval(Order price);
    }
}
>>>>>>> 704c33f55dcbb2ed9b5e5db5e52a593e8ce259ef
