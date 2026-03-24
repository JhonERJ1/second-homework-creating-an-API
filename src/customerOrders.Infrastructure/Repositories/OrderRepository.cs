using customerOrders.Domain.Entities;
using customerOrders.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Infrastructure.Repositories
{
    public class OrderRepository: GenericRepository<Order>
    {
        private readonly CustomerOrdersDbContext _context;

        public OrderRepository(CustomerOrdersDbContext context): base(context) 
        {
            _context = context;
        }

        public void Delete(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllWithCustomers()
        {

            return _context.Orders.Include(o => o.Customer).ToList();
        }


        public void UpdateOrder(Order order)
        {
            var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == order.Id);
            if (existingOrder != null)
            {
                existingOrder.CustomerId = order.CustomerId;
                //existingOrder.UpdateOrder = order.UpdateOrder;
                existingOrder.IsCanceled = order.IsCanceled;
                existingOrder.TotalAmount = order.TotalAmount;
                _context.Orders.Update(existingOrder);
            }
        }
    }
}
