using System;

namespace Demo.RabbitMQ.TopicConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Execute(args[0], args[1]);
        }
    }

    public class Client
    {
        public void Execute(string consumerName, string routingKey)
        {
            var queueName = string.Format("Test-Todd-Topic-{0}", consumerName);
            var topicConsumer = new Topic.Consumer(queueName, routingKey);
            topicConsumer.MessageReceived += TopicConsumerOnMessageReceived;

            Console.WriteLine("{0}::{1}", queueName, routingKey);
            Console.WriteLine("Press CTRL-C to end.");
            Console.WriteLine();
        }

        private void TopicConsumerOnMessageReceived(object sender, string message)
        {
            const string format = "Received '{0}'.";
            Console.WriteLine(format, message);
        }
    }
}
