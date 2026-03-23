using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using customerOrders.Persistence;
using customerOrders.Domain.Entities;
using customerOrders.Infrastructure.Repositories;
using customerOrders.Application.Responses;
using customerOrders.Application.Dtos.Orders;

namespace customerOrders.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CustomerOrdersDbContext _context;
        private readonly IMapper _mapper;
        private readonly UnitWork _unitWork;

        public OrdersController(CustomerOrdersDbContext context, 
            IMapper mapper,
            UnitWork unitWork)
        {
            _context = context;
            _mapper = mapper;
            _unitWork = unitWork;
        }

        [HttpGet]
        public ApiResponse<List<OrderDto>> GetAll()
        {
            var orders = _unitWork.OrderRepository.GetAll();
            var response = _mapper.Map<List<OrderDto>>(orders);
            return ApiResponse<List<OrderDto>>.SuccessResponse(response);
        }

        [HttpGet]
        [Route("with-customers")]
        public ApiResponse<List<OrderWithCustomerDto>> GetAllWithCustomers()
        {
  
            var orders = _unitWork.OrderRepository.GetAllWithCustomers();
            var response = _mapper.Map<List<OrderWithCustomerDto>>(orders);
            return ApiResponse<List<OrderWithCustomerDto>>.SuccessResponse(response);
        }

        [HttpGet("{id}")]
        public ApiResponse<OrderDto> GetById(int id)
        {
            var order = _unitWork.OrderRepository.GetById(id);
            if (order == null)
                return ApiResponse<OrderDto>.ErrorResponse("Order not found", 404);
            var response = _mapper.Map<OrderDto>(order);
            return ApiResponse<OrderDto>.SuccessResponse(response);
        }

        [HttpPost]
        public ApiResponse<OrderDto> Create(OrderCreateDto request)
        {
           if (request.TotalAmount <= 0)
           {
               return ApiResponse<OrderDto>.ErrorResponse("Invalid order data.", 400);
           }
           var order = _mapper.Map<Order>(request);
           order.IsCanceled = false;
            _unitWork.OrderRepository.Add(order);
            return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(order), "Order created successfully", 201);
        }

        [HttpPut("{id}")]
        public ApiResponse<OrderDto> Update(int id, OrderUpdateDto request)
        {
            if (id != request.Id || request.TotalAmount <= 0)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Invalid order data.", 400);
            }
            var existingOrder = _unitWork.OrderRepository.GetById(id);
            if (existingOrder == null)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Order not found", 404);
            }
            _unitWork.OrderRepository.UpdateOrder(_mapper.Map<Order>(request));
            return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(request), "Order updated successfully", 200);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _unitWork.OrderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            _unitWork.OrderRepository.Delete(order.Id);
            return NoContent();
        }

    }
}
