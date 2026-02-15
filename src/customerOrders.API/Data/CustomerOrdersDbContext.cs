using Microsoft.EntityFrameworkCore;

namespace customerOrders.API.Data
{
    public class CustomerOrdersDbContext: DbContext
    {
        public CustomerOrdersDbContext(DbContextOptions<CustomerOrdersDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Entities.Customer> Customers { get; set; }
        public DbSet<Models.Entities.Order> Orders { get; set; }
    }
}
