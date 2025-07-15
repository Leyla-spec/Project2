using System.Linq.Expressions;
using Project2.Core.Entities;   

namespace Project2.DAL.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task DeleteAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id, bool isTracking = false);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
