using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project2.Core.Entities;

namespace Project2.DAL.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Count).IsRequired();
            builder.HasOne(oi => oi.MenuItem)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
