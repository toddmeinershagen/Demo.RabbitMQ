using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.RabbitMQ.HeadersConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            string consumerName = string.Empty;
            bool matchAll = true;

            var headers = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++ )
            {
                if (i == 0)
                    consumerName = args[i];
                else if (i == 1)
                {
                    matchAll = Convert.ToBoolean(args[i]);
                }
                else
                {
                    var arg = args[i].Split(':');
                    var key = arg[0];
                    var value = arg[1];
                    headers.Add(key, value);
                }
            }
                
            client.Execute(consumerName, headers, matchAll);
        }
    }

    public class Client
    {
        public void Execute(string consumerName, IDictionary<string, string> headers, bool matchAll)
        {
            var queueName = string.Format("Test-Todd-Headers-{0}", consumerName);
            var topicConsumer = new Headers.Consumer(queueName, headers, matchAll);
            topicConsumer.MessageReceived += TopicConsumerOnMessageReceived;

            Console.WriteLine("{0}::{1}", queueName, headers.AsString());
            Console.WriteLine("Press CTRL-C to end.");
            Console.WriteLine();
        }

        private void TopicConsumerOnMessageReceived(object sender, string message)
        {
            const string format = "Received '{0}'.";
            Console.WriteLine(format, message);
        }
    }

    static class DictionaryExtension
    {
        public static string AsString(this IDictionary<string, string> value)
        {
            var builder = new StringBuilder();
            foreach (var item in value)
            {
                builder.AppendFormat("{0}:{1}", item.Key, item.Value);
            }

            return builder.ToString();
        }
    }
}
