namespace customerOrders.API.Models.Dtos
{
    public class OrderUpdateDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; } = 0;
        public int CustomerId { get; set; }
    }
}
