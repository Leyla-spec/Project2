using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project2.Core.Entities;

namespace Project2.DAL.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(o => o.OrderDate)
                .IsRequired();
            builder.HasOne(oi => oi .OrderItems)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
