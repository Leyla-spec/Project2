using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItem menuItem);
        Task DeleteAsync(int? id);
        Task<List<MenuItem>> GetAllAsync();
        Task<MenuItem?> GetByIdAsync(int menuId, bool isTracking = false);
        Task UpdateMenuItemAsync(MenuItem menuItem);
        Task SaveChangesAsync();

    }
}
