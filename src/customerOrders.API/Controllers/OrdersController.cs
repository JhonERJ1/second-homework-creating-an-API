using Microsoft.AspNetCore.Mvc;
namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Models.Entities.Order> _orders = new List<Models.Entities.Order>()
        {
            new Models.Entities.Order { Id = 1, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 100 },
            new Models.Entities.Order { Id = 2, OrderDate = DateTime.Now.AddDays(-5), TotalAmount = 200 },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Models.Entities.OrderDto>> GetAll()
        {
            var response = _orders.Select(o => new Models.Entities.OrderDto
            {
                Id = o.Id,
                TotalAmount = o.TotalAmount,
                CustomerId = o.CustomerId
            }).ToList();
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Models.Entities.Order> GetById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Models.Entities.Order> Create(Models.Entities.OrderDto request)
        {
           if (request.TotalAmount <= 0)
           {
               return BadRequest("Invalid order data.");
           }

            int NewId = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
            request.Id = NewId;
            request.TotalAmount = request.TotalAmount;
            var order = new Models.Entities.Order
            {
                Id = request.Id,
                OrderDate = DateTime.Now,
                UpdateOrder = DateTime.Now,
                TotalAmount = request.TotalAmount,
                CustomerId = request.CustomerId
            };
            _orders.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpPut("{id}")]
        public ActionResult<Models.Entities.Order> Update(int id, Models.Entities.OrderDto request)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.UpdateOrder = DateTime.Now;
            existingOrder.TotalAmount = request.TotalAmount;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _orders.Remove(order);
            return NoContent();
        }

    }
}
