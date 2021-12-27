using MassTransit;
using MC_Models.Models;

internal class OrderConsumer : IConsumer<Order>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderConsumer(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<Order> context)
    {
        await Console.Out.WriteLineAsync(context.Message.Name);

        await _publishEndpoint.Publish<Report>(new Report { Name = context.Message.Name, Created = DateTime.Now });

        Order test = context.Message;
    }
}