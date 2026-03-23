using Microsoft.AspNetCore.Mvc;
using customerOrders.Persistence;
using customerOrders.Domain.Entities;
using customerOrders.Infrastructure.Repositories;

namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerOrdersDbContext _context;
        private readonly UnitWork _unitWork;

        public CustomersController(CustomerOrdersDbContext context,
            UnitWork unitWork)
        {
            _context = context;
            _unitWork = unitWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _context.Customers.ToList();
            var response = _context.Customers.ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            try { 
                _unitWork.beginTransaction();
                _unitWork.CustomerRepository.AddCustomer(customer);
           
               _unitWork.Complete();
               _unitWork.commitTransaction();
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
            }
            catch (Exception)
            {
                _unitWork.rollbackTransaction();
                return StatusCode(500, "An error occurred while creating the customer.");
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Customer updateCustomer)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            customer.Name = updateCustomer.Name;
            customer.Email = updateCustomer.Email;
            customer.Phone = updateCustomer.Phone;
            customer.Address = updateCustomer.Address;
            customer.City = updateCustomer.City;
            customer.Region = updateCustomer.Region;
            customer.PostalCode = updateCustomer.PostalCode;
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}
