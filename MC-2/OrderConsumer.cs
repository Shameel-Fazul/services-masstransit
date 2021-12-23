using MassTransit;
using MC_Models.Models;

internal class OrderConsumer : IConsumer<Order>
{
    public async Task Consume(ConsumeContext<Order> context)
    {
        await Console.Out.WriteLineAsync(context.Message.Name);
    }
}