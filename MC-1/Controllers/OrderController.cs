using MassTransit;
using MassTransit.RabbitMqTransport.Integration;
using MC_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace MC_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order order)
        {
            await _publishEndpoint.Publish<Order>(order);

            return Ok();
        }
    }
}
