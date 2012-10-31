using System;

namespace Demo.RabbitMQ.FanoutPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            client.Execute();
        }
    }

    public class Client
    {
        public void Execute()
        {
            Console.WriteLine("Hit ENTER to publish a message or type 'quit' and hit ENTER to end.");
            Console.WriteLine();

            var fanoutPublisher = new Fanout.Publisher();
            var randomizer = new Random();
            var counter = 0;

            while(Console.ReadLine() != "quit")
            {
                counter++;
                const string messageFormat = "Message #{0}";
                var message = string.Format(messageFormat, counter);
                fanoutPublisher.Publish(message);

                const string consoleFormat = "Published '{0}'.";
                Console.WriteLine(consoleFormat, message);
            }
        }
    }


}
