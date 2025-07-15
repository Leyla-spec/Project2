using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(Orders entity);
        Task DeleteAsync(int? id);
        Task<List<Orders>> GetAllAsync();
        Task<Orders?> GetByIdAsync(int? id);
        Task UpdateAsync(Orders entity);
        Task<List<Orders>> GetOrderByDateAsync(DateTime dateTime);
        Task<List<Orders>> GetOrdersByDatesIntervalAsync(DateTime start, DateTime end);

    }
}

