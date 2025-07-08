using Microsoft.EntityFrameworkCore;
using Project2.Core.Entities;

namespace Project2.DAL.Contexts
{
    public class MenuandOrderDBContext :DbContext
    {
        public MenuandOrderDBContext(DbContextOptions<MenuandOrderDBContext> options) : base(options)
        {
        }

        public MenuandOrderDBContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=RestaurantOrderSystem;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
