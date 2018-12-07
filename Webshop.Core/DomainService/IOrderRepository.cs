using System.Collections.Generic;
using Webshop.Core.Entities;

namespace Webshop.Core.DomainService
{
    public interface IOrderRepository
    {
        IEnumerable<Order> ReadAllOrders();
        Order CreateOrder(Order order);
        Order DeleteOrder(int id);
        Order UpdateOrder(Order orderUpdate);
        Order FindOrderById(int id);
        Order FindOrderByIdIncludeProducts(int id);
    }
}