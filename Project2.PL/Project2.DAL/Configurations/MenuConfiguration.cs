using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project2.DAL.Configurations
{
    public class MenuConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Description).HasMaxLength(500);
            builder.Property(m => m.Price).HasColumnType("decimal(18,2)");
            builder.Property(m => m.Category).IsRequired();
        }
    }
}
