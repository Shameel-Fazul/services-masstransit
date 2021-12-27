using MassTransit;
using MC_Models.Models;
using System.Text.Json;

namespace MC_3
{
    public class OrderReportConsumer : IConsumer<Report>
    {
        private readonly IDictionary<DateTime, string> _reports;
        private readonly ILogger<OrderReportConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderReportConsumer(IDictionary<DateTime, string> reports, ILogger<OrderReportConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _reports = reports;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<Report> context)
        {
            _reports[context.Message.Created] = context.Message.Name;

            _logger.LogInformation($"{context.Message.Created.ToString()}  ->  {context.Message.Name} : [{_reports.Count}]");

            await _publishEndpoint.Publish(new Order { Name = $"{context.Message.Name}"});
            await Task.CompletedTask;
        }
    }
}
