
using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IMenuItemsInterface
    {
        public List <MenuItem> GetAll();
        public MenuItem GetMenuById(int? menuId);
        public void AddMenuItem(MenuItem menuItem);
        public void UpdateMenuItem(MenuItem menuItem);
        public void DeleteMenuItem(int? id);
    }
}
