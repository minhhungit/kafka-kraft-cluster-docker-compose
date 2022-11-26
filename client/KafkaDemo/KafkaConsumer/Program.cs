using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace KafkaConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddIniFile("getting-started.properties")
                .Build();

            configuration["group.id"] = "my-group1";
            configuration["auto.offset.reset"] = "earliest";

            const string topic = "AbcTopic";

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<string, string>(
                configuration.AsEnumerable()).Build())
            {
                consumer.Subscribe(topic);
                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume(cts.Token);
                        Console.WriteLine($"Consumed event from topic {topic} with key {cr.Message.Key,-10} and value {cr.Message.Value}");

                        Thread.Sleep(1000);
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ctrl-C was pressed.
                }
                finally
                {
                    consumer.Close();
                }
            }

            Console.WriteLine("Hello, World!");
        }
    }
}