using customerOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Persistence.EntititesConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)           
                .HasForeignKey(o => o.CustomerId) 
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
