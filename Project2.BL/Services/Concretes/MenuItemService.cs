using Project2.BL.Exceptions;
using Project2.BL.Services.Interfaces;
using Project2.Core.Entities;
using Project2.DAL.Contexts;

namespace Project2.BL.Services.Concretes
{
    public class MenuItemService : IMenuItemService
    {
        private readonly MenuAndOrderDbContext _context;
        public MenuItemService()
        {
            _context = new MenuAndOrderDbContext();
        }
        public void AddMenuItem(MenuItem menuItem)
        {
            if (_context.MenuItem.Any(m => m.Name == menuItem.Name))
                throw new DuplicateMenuItemException("Bu adla menu item artıq mövcuddur.");
            _context.MenuItem.Add(menuItem);
            _context.SaveChanges();
        }

        public void DeleteMenuItem(int? menuId)
        {
            if (menuId == null)
                throw new ArgumentNullException("Menu ID null ola bilməz.");
            var menuItem = GetMenuById(menuId);
            if (menuItem == null)
                throw new KeyNotFoundException($"ID {menuId} ilə uyğun menu item tapılmadı.");
            _context.MenuItem.Remove(menuItem);
            _context.SaveChanges();
        }

        public List<MenuItem> GetAll() => _context.MenuItem.ToList();

        public MenuItem GetMenuById(int? menuId)
        {
            if (menuId == null)
                throw new ArgumentNullException(nameof(menuId), "Menu ID null ola bilməz.");
            var menu = _context.MenuItem.SingleOrDefault(m => m.Id == menuId);
            if (menu == null)
                throw new KeyNotFoundException($"ID { menuId } ilə uyğun menu item tapılmadı.");
            return menu;
        }

        public void UpdateMenuItem(MenuItem menuItem)
        {
            if (menuItem == null)
                throw new ArgumentNullException(nameof(menuItem), "Menu item null ola bilməz.");
            var existingMenuItem = GetMenuById(menuItem.Id);
            if (existingMenuItem == null)
                throw new KeyNotFoundException($"ID { menuItem.Id } ilə uyğun menu item tapılmadı.");
            existingMenuItem.Name = menuItem.Name;
            existingMenuItem.Price = menuItem.Price;
            existingMenuItem.Category = menuItem.Category;
            _context.SaveChanges();
        }
    }
}