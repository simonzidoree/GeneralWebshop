using System.Collections.Generic;
using Webshop.Core.Entities;

namespace Webshop.Core.ApplicationService
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order CreateOrder(Order order);
        Order DeleteOrder(int id);
        Order UpdateOrder(int id, Order orderUpdate);
        Order FindOrderById(int id);
        Order FindOrderByIdIncludeProducts(int id);
    }
}