using System;
using System.Runtime.Remoting.Messaging;

namespace Demo.RabbitMQ.DirectPublisher
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

            var fanoutPublisher = new Direct.Publisher();
            var severityRandomizer = new Random();


            var counter = 0;

            while (Console.ReadLine() != "quit")
            {
                counter++;

                var app = counter % 2 == 0 ? "facebook" : "linkedin";
                var severityLevels = new string[]
                {
                    "info",
                    "warn",
                    "error",
                    "fatal"
                };

                var severityIndex = severityRandomizer.Next(3);
                var severityLevel = severityLevels[severityIndex];

                const string routingFormat = "logs.{0}.{1}";
                var routingKey = string.Format(routingFormat, app, severityLevel);

                const string messageFormat = "Message #{0} - {1}";
                var message = string.Format(messageFormat, counter, routingKey);

                fanoutPublisher.Publish(message, routingKey);

                const string consoleFormat = "Published '{0}'.";
                Console.WriteLine(consoleFormat, message);
            }
        }
    }
}
