
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RentalCompany.Core.CacheStorage;
using RentalCompany.Core.Entities;
using System.Data;
using System.Text;


namespace RentalCompany.Application.Subscriber
{
    public class MotorcyclesAvailablesSubscriber : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ICacheService _cacheService;
        private const string Queue = "available-motorcycles";
        private const string Exchange = "motorcycle-service";
        private const string RoutingKey = "available-motorcycles";

        public MotorcyclesAvailablesSubscriber(ICacheService cacheService)
        {
            _cacheService = cacheService;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("available-motorcycles-subscriber");

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(Exchange, "direct", true);
            _channel.QueueDeclare(Queue, true, false, false, null); ;
            _channel.QueueBind(Queue, "motorcycle-service", RoutingKey);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();

                var contentString = Encoding.UTF8.GetString(byteArray);
                var message = JsonConvert.DeserializeObject<List<Motorcycle>>(contentString);


                Console.WriteLine($"Availables Motorcycles received, Count: {message.Count()}");

                _cacheService.SetCache(message);

                _channel.BasicAck(eventArgs.DeliveryTag, false);

            };

            _channel.BasicConsume(Queue, false, consumer);

            return Task.CompletedTask;
        }
    }
}
