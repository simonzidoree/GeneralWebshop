using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webshop.Core.ApplicationService;
using Webshop.Core.Entities;

namespace Webshop.RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IOrderService orderService)
        {
            OrderService = orderService;
        }

        private IOrderService OrderService { get; }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            return Ok(OrderService.GetAllOrders());
        }

        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            return Ok(OrderService.CreateOrder(order));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            return Ok(OrderService.DeleteOrder(id));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.OrderId)
            {
                return BadRequest("Parameter Id and order ID must be the same");
            }

            if (order.IsDelivered && !order.EmailSent)
            {
                return Ok(OrderService.UpdateOrderAndEmail(id, order));
            }

            return Ok(OrderService.UpdateOrder(id, order));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            var order = OrderService.FindOrderByIdIncludeProducts(id);

            if (order == null)
            {
                return BadRequest($"There is no order with the ID: {id}");
            }

            return Ok(order);
        }
    }
}