using MassTransit;
using MC_3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IDictionary<DateTime, string>>(new Dictionary<DateTime, string>());

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderReportConsumer>();

    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@192.168.99.105:5672");

        cfg.ReceiveEndpoint("report-queue", c =>
        {
            c.ConfigureConsumer<OrderReportConsumer>(ctx);
        });
    });
});

builder.Services.AddMassTransitHostedService();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
