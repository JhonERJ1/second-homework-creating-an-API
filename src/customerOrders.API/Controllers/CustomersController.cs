using customerOrders.Application.Dtos.Customers;
using customerOrders.Application.Responses;
using customerOrders.Application.Services;
using customerOrders.Domain.Entities;
using customerOrders.Infrastructure.Repositories;
using customerOrders.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ClientService _clientService;

        public CustomersController(ClientService clientService )
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult Get() { 
        
                var customers = _clientService.GetAllCustomers();
                return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _clientService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post(CustomerDto custumer)
        {
            _clientService.CreateCustomer(custumer);
            return CreatedAtAction(nameof(Get), new { id = custumer.Id }, custumer);
        }

        [HttpPut("{id}")]
        public ApiResponse<CustomerDto> Put(int id, CustomerUpdateDto updateCustomer)
        {
            _clientService.UpdateCustomer(id, updateCustomer);
            return ApiResponse<CustomerDto>.SuccessResponse(null, "Customer updated successfully", 200);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clientService.DeleteCustomer(id);
            return NoContent();
        }
        
    }
}
