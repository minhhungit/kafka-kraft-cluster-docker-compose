using Confluent.Kafka;

namespace KafkaDemo
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            /*
             Need to add hosts
            127.0.0.1 kafka01
            127.0.0.1 kafka02
            127.0.0.1 kafka03


            .\kafka-producer-perf-test.bat --topic AbcTopic --num-records 1000000 --throughput -1 --producer-props bootstrap.servers=kafka01:29192,kafka02:29292,kafka03:29392 --record-size 1000
             */
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka01:29192,kafka02:29292,kafka03:29392",
                Acks = Acks.All,
                SecurityProtocol = SecurityProtocol.Plaintext
            };

            using (var p = new ProducerBuilder<string, string>(config).Build())
            {
                long id = 0;

                while (true)
                {
                    id++;
                    try
                    {
                        var dr = await p.ProduceAsync("AbcTopic", new Message<string, string> { Value = $"test {id}", Key = $"{id}" });

                        Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                    }
                    catch (ProduceException<Null, string> e)
                    {
                        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                    }
                }

            }
        }
    }
}