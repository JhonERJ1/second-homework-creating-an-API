using customerOrders.Application.Dtos.Orders;
using customerOrders.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Application.Dtos.Customers
{
    public class CustomerWithOrdersDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
