using Microsoft.EntityFrameworkCore;
using Project2.Core.Entities;

namespace Project2.DAL.Contexts
{
    public class MenuAndOrderDbContext : DbContext
    {
        public MenuAndOrderDbContext(DbContextOptions<MenuAndOrderDbContext> options) : base(options)
        {
        }

        public MenuAndOrderDbContext()
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
                optionsBuilder.UseSqlServer("Server=DESKTOP-QDOI3QL\\SQLKURS;Database=RestaurantOrderSystem;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}