using System.Collections.Generic;
using System.Linq;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.ReadAllOrders().ToList();
        }

        public Order CreateOrder(Order order)
        {
            return _orderRepository.CreateOrder(order);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepository.DeleteOrder(id);
        }

        public Order UpdateOrder(int id, Order orderUpdate)
        {
            var order = FindOrderById(id);
            order.OrderNumber = orderUpdate.OrderNumber;
            order.FullName = orderUpdate.FullName;
            order.Address = orderUpdate.Address;
            order.Zipcode = orderUpdate.Zipcode;
            order.City = orderUpdate.City;
            order.Country = orderUpdate.Country;
            order.PhoneNumber = orderUpdate.PhoneNumber;
            order.Email = orderUpdate.Email;
            order.Comment = orderUpdate.Comment;
            order.OrderDate = orderUpdate.OrderDate;
            order.IsDelivered = orderUpdate.IsDelivered;
            order.Products = orderUpdate.Products;
            return _orderRepository.UpdateOrder(order);
        }

        public Order FindOrderById(int id)
        {
            return _orderRepository.FindOrderById(id);
        }

        public Order FindOrderByIdIncludeProducts(int id)
        {
            var owner = _orderRepository.FindOrderByIdIncludeProducts(id);
            return owner;
        }
    }
}