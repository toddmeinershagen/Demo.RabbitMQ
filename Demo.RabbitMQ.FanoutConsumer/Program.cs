using System;

namespace Demo.RabbitMQ.FanoutConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Execute(args[0]);
        }
    }

    public class Client
    {
        public void Execute(string consumerName)
        {
            var fanoutConsumer = new Fanout.Consumer(string.Format("Test-Todd-Fanout-{0}", consumerName));
            fanoutConsumer.MessageReceived += FanoutConsumerOnMessageReceived;

            Console.WriteLine("Press CTRL-C to end.");
            Console.WriteLine();
        }

        private void FanoutConsumerOnMessageReceived(object sender, string message)
        {
            const string format = "Received '{0}'.";
            Console.WriteLine(format, message);
        }
    }
}
