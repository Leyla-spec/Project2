<<<<<<< HEAD
﻿using Microsoft.EntityFrameworkCore;
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
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.DAL.Configurations
{
    internal class OrderConfiguration
    {
    }
}
>>>>>>> 704c33f55dcbb2ed9b5e5db5e52a593e8ce259ef
