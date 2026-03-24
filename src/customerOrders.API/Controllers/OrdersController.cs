using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customerOrders.Persistence;
using customerOrders.Domain.Entities;
using customerOrders.Infrastructure.Repositories;
using customerOrders.Application.Responses;
using customerOrders.Application.Dtos.Orders;
using customerOrders.Application.Services;

namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ClientService _clientService;

        public OrdersController( ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public ApiResponse<List<OrderDto>> GetAll()
        {
            return _clientService.GetAllOrders();
        }

        [HttpGet]
        [Route("with-customers")]
        public ApiResponse<List<OrderWithCustomerDto>> GetAllWithCustomers()
        {
  
            return _clientService.GetAllOrdersWithCustomers();
        }

        [HttpGet("{id}")]
        public ApiResponse<OrderDto> GetById(int id)
        {
            return _clientService.GetOrderById(id);
            
        }

        [HttpPost]
        public ApiResponse<OrderDto> Create(OrderCreateDto request)
        {
            return _clientService.CreateOrder(request);
        }

        [HttpPut("{id}")]
        public ApiResponse<OrderDto> Update(int id, OrderUpdateDto request)
        {
            return _clientService.UpdateOrder(id, request);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(OrderDeleteDto request)
        {
            return (IActionResult)_clientService.DeleteOrder(request);
        }

    }
}
