using Microsoft.EntityFrameworkCore;

namespace Project2.DAL.Contexts
{
    public class MenuandOrderDBContext :DbContext
    {
        public MenuandOrderDBContext(DbContextOptions<MenuandOrderDBContext> options) : base(options)
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
    }
}
