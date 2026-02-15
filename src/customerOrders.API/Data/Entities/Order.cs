using System.ComponentModel.DataAnnotations;
namespace customerOrders.API.Models.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
       // public DateTime OrderDate { get; set; }

      //  public DateTime UpdateOrder { get; set; }
        
        public decimal TotalAmount { get; set; } = 0;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
