using AutoMapper;
using customerOrders.Application.Dtos.Customers;
using customerOrders.Application.Dtos.Orders;
using customerOrders.Application.Responses;
using customerOrders.Domain.Entities;
using customerOrders.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace customerOrders.Application.Services
{
    public class ClientService
    {
        private readonly UnitWork _unitWork;

        private readonly IMapper _mapper;

        public ClientService(UnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        public ApiResponse<CustomerDto> GetCustomerById(int id)
        {
            var entity = _unitWork.CustomerRepository.GetCustomerById(id);
            if (entity == null)
            {
                return ApiResponse<CustomerDto>.ErrorResponse("Customer not found");
            }
            var response = _mapper.Map<CustomerDto>(entity);
            return ApiResponse<CustomerDto>.SuccessResponse(response, "Customer succesfully", 200);
        }


        public ApiResponse<CustomerDto> CreateCustomerWithOrders(CustomerWithOrdersDto request)
        {
            if (request == null)
            {
                return ApiResponse<CustomerDto>.ErrorResponse("Invalid customer data", 400);
            }

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Email))
            {
                return ApiResponse<CustomerDto>.ErrorResponse("Name and Email are required", 400);
            }

            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                Region = request.Region,
                PostalCode = request.PostalCode

            };
            try
            {
                _unitWork.beginTransaction();
                _unitWork.CustomerRepository.AddCustomer(customer);

                _unitWork.Complete();
                _unitWork.commitTransaction();
            }
            catch (Exception)
            {
                _unitWork.rollbackTransaction();
                throw;
            }
            return ApiResponse<CustomerDto>.SuccessResponse(_mapper.Map<CustomerDto>(customer), "Customer created successfully", 201);
        }

        public ApiResponse<List<OrderWithCustomerDto>> GetAllOrdersWithCustomers()
        {
            var orders = _unitWork.OrderRepository.GetAllWithCustomers();
            var response = _mapper.Map<List<OrderWithCustomerDto>>(orders);
            return ApiResponse<List<OrderWithCustomerDto>>.SuccessResponse(response, "Orders with customers retrieved successfully", 200);
        }

        public void CreateCustomer(CustomerDto custumer)
        {
                _unitWork.CustomerRepository.AddCustomer(_mapper.Map<Customer>(custumer));
                _unitWork.Complete();
        }

        public void UpdateCustomer(int id, CustomerDto updateCustomer)
        {
            var existingCustomer = _unitWork.CustomerRepository.GetCustomerById(id);
            if (existingCustomer == null)
                throw new Exception("Customer not found");
            _mapper.Map(updateCustomer, existingCustomer);
            existingCustomer.Id = id; 
            _unitWork.CustomerRepository.UpdateCustomer(existingCustomer);
            _unitWork.Complete();
        }

        public void DeleteCustomer(int id)
        {
            var existingCustomer = _unitWork.CustomerRepository.GetCustomerById(id);
            if (existingCustomer == null)
                throw new Exception("Customer not found");

            _unitWork.CustomerRepository.DeleteCustomer(id);
            _unitWork.Complete();
        }

        public ApiResponse<List<OrderDto>> GetAllOrders()
        {
            var orders = _unitWork.OrderRepository.GetAll();
            var response = _mapper.Map<List<OrderDto>>(orders);
            return ApiResponse<List<OrderDto>>.SuccessResponse(response, "Orders retrieved successfully", 200);
        }

        public ApiResponse<OrderDto> GetOrderById(int id)
        {
            var order = _unitWork.OrderRepository.GetById(id);
            if (order == null)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Order not found", 404);
            }
            var response = _mapper.Map<OrderDto>(order);
            return ApiResponse<OrderDto>.SuccessResponse(response, "Order retrieved successfully", 200);
        }

        public ApiResponse<OrderDto> CreateOrder(OrderCreateDto request)
        {
            if (request == null || request.CustomerId <= 0 || request.TotalAmount <= 0)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Invalid order data", 400);
            }
            var customer = _unitWork.CustomerRepository.GetCustomerById(request.CustomerId);
            if (customer == null)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Customer not found", 404);
            }
            var order = new Order
            {
                TotalAmount = request.TotalAmount,
                CustomerId = request.CustomerId,
                IsCanceled = false,
            };
            _unitWork.OrderRepository.Add(order);
            return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(order), "Order created successfully", 201);
        }

        public ApiResponse<OrderDto> UpdateOrder(int id, OrderUpdateDto request)
        {
            var order = _unitWork.OrderRepository.GetById(id);
            if (order == null)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Order not found", 404);
            }
            if (request.TotalAmount <= 0)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Invalid order data", 400);
            }
            order.TotalAmount = request.TotalAmount;
            _unitWork.OrderRepository.Update(order);
            return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(order), "Order updated successfully", 200);
        }

        public ApiResponse<OrderDto> DeleteOrder(OrderDeleteDto request)
        {
            var order = _unitWork.OrderRepository.GetById(request.Id);
            if (order == null)
            {
                return ApiResponse<OrderDto>.ErrorResponse("Order not found", 404);
            }
            order.IsCanceled = true;
            _unitWork.OrderRepository.Update(order);
            return ApiResponse<OrderDto>.SuccessResponse(_mapper.Map<OrderDto>(order), "Order canceled successfully", 200);
        }

        public ApiResponse<List<CustomerDto>> GetAllCustomers()
        {
            var customers = _unitWork.CustomerRepository.GetAllCustomers();
            var response = _mapper.Map<List<CustomerDto>>(customers);
            return ApiResponse<List<CustomerDto>>.SuccessResponse(response, "Customers retrieved successfully", 200);
        }
    }
}
