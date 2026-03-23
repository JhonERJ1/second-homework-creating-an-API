using customerOrders.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Infrastructure.Repositories
{
    public class UnitWork
    {

        private readonly CustomerOrdersDbContext _context;
        public CustomerRepository CustomerRepository { get; private set; }
        public OrderRepository OrderRepository { get; private set; }

        public UnitWork(CustomerOrdersDbContext context,
            OrderRepository orderRepository,
            CustomerRepository customerRepository) 
        { 
            _context = context;
            this.CustomerRepository = customerRepository;
            this.OrderRepository = orderRepository;
        }


        public void beginTransaction()
        {
           _context.Database.BeginTransaction();
        }

        public void commitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void rollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
