using System;
using Demo.RabbitMQ.Serializers;
using RabbitMQ.Client;

namespace Demo.RabbitMQ.Fanout
{
    public class Publisher : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ExchangeName = "Test-Todd-Fanout";
        private const int NonPersistent = 1;
        private const int Persistent = 2;

        public Publisher()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, "fanout", true);
        }

        public void Publish(string message)
        {
            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = Persistent;

            var serializer = new BinarySerializer<string>();
            var bytes = serializer.Serialize(message);
            _channel.BasicPublish(ExchangeName, string.Empty, true, false, properties, bytes);
        }

        public void Dispose()
        {
            if (_channel != null)
                _channel.Close();

            if (_connection != null)
            _connection.Close();
        }
    }
}