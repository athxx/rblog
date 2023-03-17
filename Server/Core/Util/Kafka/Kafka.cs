using Confluent.Kafka;

namespace Core.Util;

public class KafkaClient : IDisposable
{
    private readonly IProducer<string, string> _producer;
    private readonly IConsumer<string, string> _consumer;

    public KafkaClient(string bootstrapServers, string groupId)
    {
        var prdCfg = new ProducerConfig { BootstrapServers = bootstrapServers };
        _producer = new ProducerBuilder<string, string>(prdCfg).Build();

        var csmCfg = new ConsumerConfig { BootstrapServers = bootstrapServers, GroupId = groupId };
        _consumer = new ConsumerBuilder<string, string>(csmCfg).Build();
    }

    public async Task ProduceAsync(string topic, string message)
    {
        var result = await _producer.ProduceAsync(topic,
            new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
        Console.WriteLine($"Produced message to '{result.TopicPartitionOffset}'");
    }

    public void Consume(string topic, Action<string> action)
    {
        _consumer.Subscribe(topic);
        while (true)
        {
            var result = _consumer.Consume();
            if (result.IsPartitionEOF)
            {
                Console.WriteLine($"Reached end of partition '{result.TopicPartition}'");
                continue;
            }

            Console.WriteLine($"Consumed message '{result.Message.Value}' at: '{result.TopicPartitionOffset}'");
            action(result.Message.Value);
        }
    }

    public void Dispose()
    {
        _producer?.Dispose();
        _consumer?.Dispose();
    }
}