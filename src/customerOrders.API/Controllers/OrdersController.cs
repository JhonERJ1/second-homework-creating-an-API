using customerOrders.API.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly Data.CustomerOrdersDbContext _context;

        public OrdersController(Data.CustomerOrdersDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Models.Dtos.OrderWithCustomerDto>> GetAll()
        {
           
            
            var response = _context.Orders.Include(o => o.Customer)
                .Select(o => new Models.Dtos.OrderWithCustomerDto
            {
                Id = o.Id,
                TotalAmount = o.TotalAmount,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.Name
            }).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Dtos.OrderWithCustomerDto> GetById(int id)
        {
            var response = _context.Orders.Include(o => o.Customer)
                .Where(o => o.Id == id)
                .Select(o => new Models.Dtos.OrderWithCustomerDto
                {
                Id = o.Id,
                TotalAmount = o.TotalAmount,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.Name
            }).FirstOrDefault();

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<Models.Entities.Order> Create(Models.Dtos.OrderCreateDto request)
        {
           if (request.TotalAmount <= 0)
           {
               return BadRequest("Invalid order data.");
           }
            request.TotalAmount = request.TotalAmount;
            var order = new Models.Entities.Order
            {
                //OrderDate = DateTime.Now,
                //UpdateOrder = DateTime.Now,
                TotalAmount = request.TotalAmount,
                CustomerId = request.CustomerId
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(new { id = order.Id });
            
            //return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public ActionResult<Models.Entities.Order> Update(int id, Models.Dtos.OrderUpdateDto request)
        {
            if (id != request.Id || request.TotalAmount <= 0)
            {
                return BadRequest("Invalid order data.");
            }
            var existingOrder = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            //existingOrder.UpdateOrder = DateTime.Now;
            existingOrder.TotalAmount = request.TotalAmount;
            existingOrder.CustomerId = request.CustomerId;
            
            _context.Orders.Update(existingOrder);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
