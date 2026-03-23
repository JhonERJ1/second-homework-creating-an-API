using customerOrders.API.Models.Dtos;
using customerOrders.Domain.Entities;

namespace customerOrders.API.Models
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
