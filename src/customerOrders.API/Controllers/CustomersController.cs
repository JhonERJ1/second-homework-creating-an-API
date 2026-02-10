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
        private static List<Models.Customer> _customers = new List<Models.Customer>()
            {
                new Models.Customer { Id = 1, Name = "John Doe", Email = "JDoe@email.com" , Phone = "800-000-0000", Address = "C/4, piantini No. 25, Capital, RD",
                City = "Capital", Region = "Republica Dominicana", PostalCode = "00000"},
                new Models.Customer { Id = 2, Name = "Jane Smith", Email = "JSmith@email.com", Phone = "811-111-1111", Address = "C/6, Gold street, Dubai",
                City = "Capital de dubai", Region = "Dubai", PostalCode = "12214" },
            };

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customers);
        }
        [HttpGet("/api/orders/Get1")]
        public IActionResult Getxx()
        {
            return Ok(_customers);
        }
        [HttpGet("Get2")]
        public IActionResult Getx()
        {
            return Ok(_customers);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Models.Customer customer)
        {
            customer.Id = _customers.Max(m => m.Id) + 1;
            _customers.Add(customer);
            return CreatedAtAction(nameof(Get), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Models.Customer updateCustomer)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
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
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customers.Find(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _customers.Remove(customer);
            return NoContent();
        }
        
    }
}
