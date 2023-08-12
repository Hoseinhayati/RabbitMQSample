using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Messages;

namespace RabbitMQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IBus _bus;

        public OrderController(IBus bus)
        {
            _bus = bus;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOrder([FromBody] OrderCreated orderModel)
        //{
        //    var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/order-created"));
        //    await endpoint.Send(new OrderCreated { OrderId = orderModel.OrderId });

        //    return Ok("Order created and message sent.");
        //}

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreated orderModel)
        {
            // Send a message using MassTransit
            await _bus.Publish(new OrderCreated { Name = orderModel.Name });

            return Ok("Order created and message sent.");
        }

    }
}
