using MassTransit;
using MC_Models.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@192.168.99.105:5672");

        cfg.ReceiveEndpoint("order-queue", c =>
        {
            c.ConfigureConsumer<OrderConsumer>(ctx);
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var bus = Bus.Factory.CreateUsingRabbitMq(config =>
//{
//    config.Host("amqp://guest:guest@192.168.99.105:5672");

//    config.ReceiveEndpoint("temp-queue", c =>
//    {
//        c.Handler<Order>(ctx =>
//        {
//            return Console.Out.WriteLineAsync(ctx.Message.Name);
//        });
//    });
//});

//bus.Start();

app.Run();
