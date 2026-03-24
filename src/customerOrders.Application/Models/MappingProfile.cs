using customerOrders.Application.Dtos;
using customerOrders.Application.Dtos.Customers;
using customerOrders.Application.Dtos.Orders;
using customerOrders.Domain.Entities;

namespace customerOrders.Application.Models
{
    public class MappingProfile: AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, OrderCreateDto>().ReverseMap();
            CreateMap<Order, OrderWithCustomerDto>().ReverseMap();
                

        }
    }
}
