using Microsoft.EntityFrameworkCore;
using customerOrders.Domain.Entities;
using customerOrders.Persistence.EntititesConfiguration;

namespace customerOrders.Persistence
{
    public class CustomerOrdersDbContext : DbContext
    {
        public CustomerOrdersDbContext(DbContextOptions<CustomerOrdersDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
    }
}
