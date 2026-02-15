using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;
namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly Data.CustomerOrdersDbContext _context;

        public CustomersController(Data.CustomerOrdersDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _context.Customers.ToList();
            return Ok(customers);
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
        public IActionResult Post(Models.Entities.Customer customer)
        {
            customer.Id = _context.Customers.Max(m => m.Id) + 1;
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Models.Entities.Customer updateCustomer)
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
