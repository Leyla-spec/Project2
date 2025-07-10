using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IMenuItemService
    {
        public void AddMenuItem(MenuItem menuItem);
        public void DeleteMenuItem(int? menuId);
        public List<MenuItem> GetAll();
        public MenuItem GetMenuById(int? menuId);
        public void UpdateMenuItem(MenuItem menuItem);

    }
}
