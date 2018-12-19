using System;
using System.Collections.Generic;
using Moq;
using Webshop.Core.ApplicationService;
using Webshop.Core.ApplicationService.Services;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;
using Xunit;

namespace Webshop.xUnitTests.Core.ApplicationService.Services
{
    public class OrderServiceTest
    {
        private Mock<IOrderRepository> CreateOrderMockRepository()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    OrderId = 1,
                    OrderNumber = 23534534,
                    FullName = "FM",
                    Address = "A 404",
                    Zipcode = 6700,
                    City = "City",
                    Country = "Denmark",
                    PhoneNumber = 35335353,
                    Email = "e@e.com",
                    Comment = "Nice comment",
                    OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                    IsDelivered = false
                },
                new Order
                {
                    OrderId = 2,
                    OrderNumber = 23434534,
                    FullName = "aa",
                    Address = "ggg 4",
                    Zipcode = 6700,
                    City = "City",
                    Country = "Denmark",
                    PhoneNumber = 56556565,
                    Email = "a@a.com",
                    Comment = "Nice comment",
                    OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                    IsDelivered = true
                }
            };


            var repository = new Mock<IOrderRepository>();
            repository.Setup(r => r.ReadAllOrders())
                .Returns(orders);

            repository.Setup(x => x.FindOrderById(1)).Returns(orders[0]);

            return repository;
        }

        [Theory]
        [InlineData(1, 1)]
        private void GetOrderById(int id, int expected)
        {
            var repository = CreateOrderMockRepository();
            IOrderService service = new OrderService(repository.Object);

            var actual = service.FindOrderById(id).OrderId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAllOrderCountThrowsNoException()
        {
            var mockOrderRepo = CreateOrderMockRepository();
            IOrderService service = new OrderService(mockOrderRepo.Object);

            var expectedOrdersCount = 2;
            var actualOrdersCount = service.GetAllOrders().Count;

            Assert.Equal(expectedOrdersCount, actualOrdersCount);
        }

        [Fact]
        public void VerifyCreateOrderCallOrderRepoCreateOrderOnce()
        {
            var mockOrderRepo = new Mock<IOrderRepository>();
            IOrderService service = new OrderService(mockOrderRepo.Object);

            var order = new Order
            {
                OrderId = 1,
                OrderNumber = 23534534,
                FullName = "FM",
                Address = "A 404",
                Zipcode = 6700,
                City = "City",
                Country = "Denmark",
                PhoneNumber = 35335353,
                Email = "e@e.com",
                Comment = "Nice comment",
                OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                IsDelivered = false
            };


            service.CreateOrder(order);
            mockOrderRepo.Verify(x => x.CreateOrder(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void VerifyDeleteOrderCallOrderRepoDeleteOrderOnce()
        {
            var mockOrderRepo = new Mock<IOrderRepository>();
            IOrderService service = new OrderService(mockOrderRepo.Object);

            var order = new Order
            {
                OrderId = 1,
                OrderNumber = 23534534,
                FullName = "FM",
                Address = "A 404",
                Zipcode = 6700,
                City = "City",
                Country = "Denmark",
                PhoneNumber = 35335353,
                Email = "e@e.com",
                Comment = "Nice comment",
                OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                IsDelivered = false
            };


            service.DeleteOrder(order.OrderId);
            mockOrderRepo.Verify(x => x.DeleteOrder(order.OrderId), Times.Once);
        }

        [Fact]
        public void VerifyUpdateOrderCallOrderRepoUpdateOrderOnce()
        {
            var mockOrderRepo = CreateOrderMockRepository();
            IOrderService service = new OrderService(mockOrderRepo.Object);

            var order = new Order
            {
                OrderId = 1,
                OrderNumber = 23534534,
                FullName = "FM",
                Address = "A 404",
                Zipcode = 6700,
                City = "City",
                Country = "Denmark",
                PhoneNumber = 35335353,
                Email = "e@e.com",
                Comment = "Nice comment",
                OrderDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                IsDelivered = false
            };


            service.UpdateOrder(order.OrderId, order);
            mockOrderRepo.Verify(x => x.UpdateOrder(It.IsAny<Order>()), Times.Once);
        }
    }
}