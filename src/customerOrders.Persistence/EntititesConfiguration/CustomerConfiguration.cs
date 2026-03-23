using customerOrders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Persistence.EntititesConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(200);
            builder.Property(e => e.City).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Region).IsRequired().HasMaxLength(50);
            builder.Property(e => e.PostalCode).IsRequired().HasMaxLength(20);
        }
        
    }
}