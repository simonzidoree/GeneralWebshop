using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public Order UpdateOrderAndEmail(int id, Order orderUpdate)
        {
            try
            {
                var client = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials =
                        new NetworkCredential("username", "password")
                };

                var mm = new MailMessage("from", "to", "Test email",
                    "This is a test email.");
                mm.BodyEncoding = Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
                mm.Dispose();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
            }

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
            order.EmailSent = true;
            order.Products = orderUpdate.Products;
            return _orderRepository.UpdateOrder(order);
        }
    }
}