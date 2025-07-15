using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project2.BL.Exceptions;
using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;
using Project2.DAL.Repositories.Conceretes;
using Project2.DAL.Repositories.Interfaces;

namespace Project2.BL.Services.Concretes
{
    public class MenuItemService : IMenuItemService
    {
        private readonly Repositories<MenuItem> _context;
        public MenuItemService(MenuAndOrderDbContext context)
        {
            _context = new Repositories<MenuItem>(context);
        }

        public async Task AddMenuItemAsync(MenuItem menuItem)
        {
            var allItems = await _context.GetAllAsync();
            if (allItems.Any(m => m.Name == menuItem.Name))
                throw new DuplicateMenuItemException("Bu adla menu item artıq mövcuddur.");

            await _context.AddAsync(menuItem);
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "ID null ola bilməz.");

            var entity = await _context.GetByIdAsync(id.Value);
            if (entity == null)
                throw new KeyNotFoundException($"ID {id} ilə uyğun entiti tapılmadı.");

            await _context.DeleteAsync(id);
        }

        public async Task<List<MenuItem>> GetAllAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<MenuItem?> GetByIdAsync(int menuId, bool isTracking = false)
        {
            var menu = await _context.GetByIdAsync(menuId, isTracking);
            if (menu == null)
                throw new KeyNotFoundException($"ID {menuId} ilə uyğun menu item tapılmadı.");

            return menu;
        }

        public async Task UpdateMenuItemAsync(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new ArgumentNullException(nameof(menuItem), "Menu item null ola bilməz.");

            var existingMenuItem = await _context.GetByIdAsync(menuItem.Id);
            if (existingMenuItem == null)
                throw new KeyNotFoundException($"ID {menuItem.Id} ilə uyğun menu item tapılmadı.");

            await _context.UpdateAsync(menuItem);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }

}