using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project2.Core.Entities;

namespace Project2.DAL.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).
                IsRequired().
                HasMaxLength(100);
            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(m => m.Category).
                IsRequired();
        }
    }
}
