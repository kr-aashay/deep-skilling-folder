using Confluent.Kafka;

namespace KafkaChatApp;

// Kafka Consumer — receives chat messages from a Kafka topic
public class KafkaConsumer
{
    private readonly string _bootstrapServers;
    private readonly string _topic;
    private readonly string _groupId;

    public KafkaConsumer(string bootstrapServers, string topic, string groupId)
    {
        _bootstrapServers = bootstrapServers;
        _topic            = topic;
        _groupId          = groupId;
    }

    public void Start(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId          = _groupId,
            AutoOffsetReset  = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(_topic);

        Console.WriteLine($"[Consumer] Listening on topic: {_topic}\n");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);
                Console.WriteLine($"[Chat] {result.Message.Value}");
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
