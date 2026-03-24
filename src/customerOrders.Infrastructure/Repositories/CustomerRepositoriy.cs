using customerOrders.Domain.Entities;
using customerOrders.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Infrastructure.Repositories
{
    public class CustomerRepository
    {
        private readonly CustomerOrdersDbContext _context;

        public CustomerRepository(CustomerOrdersDbContext context)
        {
            _context = context;
        }
        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }
        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(o => o.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void GetCustomerWithOrders(int id)
        {
            var customer = _context.Customers.FirstOrDefault(o => o.Id == id);
            if (customer != null)
            {
                var orders = _context.Orders.Where(o => o.CustomerId == id).ToList();
                customer.Orders = orders;
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = _context.Customers.FirstOrDefault(o => o.Id == customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
                existingCustomer.City = customer.City;
                existingCustomer.Region = customer.Region;
                existingCustomer.PostalCode = customer.PostalCode;
                _context.Customers.Update(existingCustomer);
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(o => o.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }
        }
    }
}
