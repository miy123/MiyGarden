using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using SecurityProtocol = Confluent.Kafka.SecurityProtocol;

namespace MiyGarden.Service.Others
{
    public class LdrKafkaRepository
    {
        private static int _key;
        private static readonly object Obj = new();
        private readonly ProducerConfig _producerConfig;

        public LdrKafkaRepository(string connectionStr)
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = connectionStr,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslPlaintext,
                SaslUsername = "devapp",
                SaslPassword = "app.123"
            };
        }

        public async Task Add(string topic, string key, string message)
        {
            using var producer = new ProducerBuilder<string, string>(_producerConfig).Build();
            var response = await producer.ProduceAsync(topic,
                new Message<string, string>
                {
                    Key = key,
                    Value = message
                });
        }

        public async Task Add(string topic, IEnumerable<LdrPlaceBet> messages)
        {
            using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                Parallel.ForEach(messages, message =>
                {
                    producer.ProduceAsync(topic,
                        new Message<string, string>
                        {
                            Key = message.BetNo.ToString(),
                            Value = JsonSerializer.Serialize(message)
                        });
                });

                producer.Flush();
            }

            await Task.CompletedTask;
        }

        public async Task Add(string topic, IEnumerable<LdrSettleBet> messages)
        {
            using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                Parallel.ForEach(messages, message =>
                {
                    producer.ProduceAsync(topic,
                        new Message<string, string>
                        {
                            Key = message.BetNo.ToString(),
                            Value = JsonSerializer.Serialize(message)
                        });
                });

                producer.Flush();
            }

            await Task.CompletedTask;
        }

        public async Task Add<T>(string topic, IEnumerable<T> messages)
        {
            using (var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
            {
                Parallel.ForEach(messages, message =>
                {
                    producer.ProduceAsync(topic,
                        new Message<string, string>
                        {
                            Key = GetKey().ToString(),
                            Value = JsonSerializer.Serialize(message)
                        });
                });

                producer.Flush();
            }

            await Task.CompletedTask;
        }


        private static int GetKey()
        {
            lock (Obj)
            {
                return _key++;
            }
        }

        public async Task Clean(string[] topics)
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig(_producerConfig)).Build();
            try
            {
                await adminClient.DeleteTopicsAsync(topics);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await Task.CompletedTask;
        }
    }

}
