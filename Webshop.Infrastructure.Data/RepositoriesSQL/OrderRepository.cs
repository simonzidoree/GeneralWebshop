using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webshop.Core.DomainService;
using Webshop.Core.Entities;

namespace Webshop.Infrastructure.Data.RepositoriesSQL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly WebshopContext _ctx;

        public OrderRepository(WebshopContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Order> ReadAllOrders()
        {
            return _ctx.Orders;
        }

        public Order CreateOrder(Order order)
        {
            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        public Order DeleteOrder(int id)
        {
            var orderRemoved = _ctx.Remove(new Order {OrderId = id}).Entity;
            _ctx.SaveChanges();
            return orderRemoved;
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            var newOrderLines = new List<OrderLine>(orderUpdate.OrderLines);
            _ctx.Attach(orderUpdate).State = EntityState.Modified;
            _ctx.OrderLines.RemoveRange(
                _ctx.OrderLines.Where(ol => ol.OrderId == orderUpdate.OrderId)
            );

            foreach (var ol in newOrderLines)
            {
                _ctx.Entry(ol).State = EntityState.Added;
            }

            _ctx.SaveChanges();
            return orderUpdate;
        }

        public Order FindOrderById(int id)
        {
            return _ctx.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public Order FindOrderByIdIncludeProducts(int id)
        {
            return _ctx.Orders.Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Product)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public int GetLastOrderNumber()
        {
            try
            {
                return _ctx.Orders.Select(order => order.OrderNumber).Max();
            }
            catch (Exception)
            {
                return 23534534;
            }
        }
    }
}