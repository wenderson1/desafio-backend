using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using User.Application.Interfaces;
using System.Text;
using Newtonsoft.Json;
using User.Core.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace User.Application.Subscriber
{
    public class UpdateReturnMotorcycleSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private const string Queue = "returned-motorcycle";
        private const string Exchange = "order-service";    
        private const string RoutingKey = "order-service";

        public UpdateReturnMotorcycleSubscriber(IServiceProvider serviceProvider)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("update-motorcycles-subscriber");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(Exchange, "direct", true);
            _channel.QueueDeclare(Queue, true, false, false, null); ;
            _channel.QueueBind(Queue, "order-service", RoutingKey);
            _serviceProvider = serviceProvider;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<string>(contentString);

                Console.WriteLine($"Motorcycles received, motorcycle: {message}");

                await UpdateMotorcycle(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }

        private async Task<bool> UpdateMotorcycle(string message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var motorcycleService = scope.ServiceProvider.GetService<IMotorcycleService>();

                await motorcycleService.UpdateAvailabilityMotorcycle(message, MotorcycleStatusEnum.Available);
            }

            return true;
        }
    }
}
