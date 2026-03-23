namespace customerOrders.Application.Dtos.Orders
{
    public class OrderWithCustomerDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; } = 0;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
