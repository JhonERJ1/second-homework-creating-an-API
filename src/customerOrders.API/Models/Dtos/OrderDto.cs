namespace customerOrders.API.Models.Entities
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; } = 0;

        public int CustomerId { get; set; }
    }
}
