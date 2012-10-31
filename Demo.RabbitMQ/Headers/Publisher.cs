using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Demo.RabbitMQ.Serializers;
using RabbitMQ.Client;

namespace Demo.RabbitMQ.Headers
{
    public class Publisher : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ExchangeName = "Test-Todd-Headers";
        private const int NonPersistent = 1;
        private const int Persistent = 2;

        public Publisher()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, "headers", true);
        }

        public void Publish(string message, string routingKey, IDictionary<string, string> headers)
        {
            var properties = _channel.CreateBasicProperties();
            properties.DeliveryMode = Persistent;
            properties.Headers = new HybridDictionary();

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    properties.Headers.Add(item.Key, item.Value);        
                }
            }
            
            var serializer = new BinarySerializer<string>();
            var bytes = serializer.Serialize(message);
            _channel.BasicPublish(ExchangeName, routingKey, true, false, properties, bytes);
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
