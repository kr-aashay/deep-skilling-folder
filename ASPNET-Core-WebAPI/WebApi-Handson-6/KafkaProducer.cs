using Confluent.Kafka;

namespace KafkaChatApp;

// Kafka Producer — sends chat messages to a Kafka topic
public class KafkaProducer
{
    private readonly string _bootstrapServers;
    private readonly string _topic;

    public KafkaProducer(string bootstrapServers, string topic)
    {
        _bootstrapServers = bootstrapServers;
        _topic            = topic;
    }

    public async Task StartAsync()
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        Console.WriteLine($"[Producer] Connected to Kafka. Topic: {_topic}");
        Console.WriteLine("[Producer] Type messages to send. Type 'exit' to quit.\n");

        while (true)
        {
            Console.Write("You: ");
            var message = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(message) || message.ToLower() == "exit")
                break;

            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
            Console.WriteLine($"[Producer] Message sent: {message}");
        }
    }
}
