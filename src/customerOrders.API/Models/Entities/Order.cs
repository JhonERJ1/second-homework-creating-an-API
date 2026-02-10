namespace customerOrders.API.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; } = 0;
        public Customer? Customer { get; set; }

    }
}
