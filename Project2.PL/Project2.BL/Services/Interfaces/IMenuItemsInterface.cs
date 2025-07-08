<<<<<<< HEAD
﻿
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
=======
﻿
using Project2.Core.Entities;

namespace Project2.BL.Services.Interfaces
{
    public interface IMenuItemsInterface
    {
        public List <MenuItem> GetAll();
        public MenuItem GetMenuById();
        public void AddMenuItem(MenuItem menuItem);
        public void UpdateMenuItem(MenuItem menuItem);
        public void DeleteMenuItem(int id);
    }
}
>>>>>>> 704c33f55dcbb2ed9b5e5db5e52a593e8ce259ef
