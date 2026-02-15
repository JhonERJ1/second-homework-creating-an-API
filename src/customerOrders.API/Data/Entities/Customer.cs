using System.ComponentModel.DataAnnotations;
namespace customerOrders.API.Models.Entities
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
        [StringLength(50)]
        public string Phone { get; set; } = string.Empty;
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;
        [StringLength(100)]
        public string City { get; set; } = string.Empty;
        [StringLength(100)]
        public string Region { get; set; } = string.Empty;
        [StringLength(20)]
        public string PostalCode { get; set; } = string.Empty;
    }
}
