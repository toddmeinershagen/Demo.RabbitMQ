using System;
using System.Collections.Generic;
using Demo.RabbitMQ.Serializers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Demo.RabbitMQ.Fanout
{
    public class Consumer : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string ExchangeName = "Test-Todd-Fanout";
        private readonly EventingBasicConsumer _consumer;
        public event EventHandler<string> MessageReceived; 

        public Consumer(string queueName)
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            var args = new Dictionary<String, Object>();
            _channel.ExchangeDeclare(ExchangeName, "fanout", true);
            _channel.QueueDeclare(queueName, true, false, false, args);
            _channel.QueueBind(queueName, ExchangeName, "");

            _consumer = new EventingBasicConsumer();
            _consumer.Received += ConsumerReceived;
            _channel.BasicConsume(queueName, true, _consumer);
        }

        private void ConsumerReceived(IBasicConsumer sender, BasicDeliverEventArgs args)
        {
            var serializer = new BinarySerializer<string>();
            var message = serializer.DeSerialize(args.Body);

            OnMessageReceived(message);
        }

        private void OnMessageReceived(string message)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, message);
            }
        }

        public void Dispose()
        {
            if (_connection != null)
                _connection.Close();

            if (_channel != null)
                _channel.Close();
        }
    }
}
