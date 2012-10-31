using System;

namespace Demo.RabbitMQ.DirectConsumer
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
            var queueName = string.Format("Test-Todd-Direct-{0}", consumerName);
            var directConsumer = new Direct.Consumer(queueName, routingKey);
            directConsumer.MessageReceived += DirectConsumerOnMessageReceived;

            Console.WriteLine("{0}::{1}", queueName, routingKey);
            Console.WriteLine("Press CTRL-C to end.");
            Console.WriteLine();
        }

        private void DirectConsumerOnMessageReceived(object sender, string message)
        {
            const string format = "Received '{0}'.";
            Console.WriteLine(format, message);
        }
    }
}
