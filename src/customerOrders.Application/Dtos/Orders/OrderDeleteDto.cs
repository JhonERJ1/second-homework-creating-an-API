using customerOrders.Domain.Entities;

namespace customerOrders.Application.Dtos.Orders
{
    public class OrderDeleteDto
    {
        public int Id { get; set; }
        //public DateTime OrderDate { get; set; }

        //public DateTime UpdateOrder { get; set; }

        public decimal TotalAmount { get; set; } = 0;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsCanceled { get; set; }
    }
}
