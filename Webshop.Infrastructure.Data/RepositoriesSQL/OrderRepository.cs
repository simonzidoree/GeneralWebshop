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
            var orderFromDb = _ctx.Orders.Add(order).Entity;
            _ctx.SaveChanges();
            return orderFromDb;
        }

        public Order DeleteOrder(int id)
        {
            var orderRemoved = _ctx.Remove(new Order {Id = id}).Entity;
            _ctx.SaveChanges();
            return orderRemoved;
        }

        public Order UpdateOrder(Order orderUpdate)
        {
            _ctx.Attach(orderUpdate).State = EntityState.Modified;
            _ctx.SaveChanges();

            return orderUpdate;
        }

        public Order FindOrderById(int id)
        {
            return _ctx.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order FindOrderByIdIncludeProducts(int id)
        {
            return _ctx.Orders
                .Include(o => o.Products)
                .FirstOrDefault(o => o.Id == id);
        }
    }
}