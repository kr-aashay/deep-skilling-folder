using KafkaChatApp;

// Kafka Chat Application
// Prerequisites:
// 1. Start Zookeeper: zookeeper-server-start.bat ../../config/zookeeper.properties
// 2. Start Kafka:     kafka-server-start.bat ../../config/server.properties
// 3. Create topic:    kafka-topics.bat --create --topic chat-topic --bootstrap-server localhost:9092

const string bootstrapServers = "localhost:9092";
const string topic            = "chat-topic";
const string groupId          = "chat-group";

Console.WriteLine("=== Kafka Chat Application ===");
Console.WriteLine("Select mode:");
Console.WriteLine("  1 - Producer (send messages)");
Console.WriteLine("  2 - Consumer (receive messages)");
Console.Write("Enter choice: ");

var choice = Console.ReadLine();

if (choice == "1")
{
    var producer = new KafkaProducer(bootstrapServers, topic);
    await producer.StartAsync();
}
else if (choice == "2")
{
    var cts      = new CancellationTokenSource();
    var consumer = new KafkaConsumer(bootstrapServers, topic, groupId);

    Console.CancelKeyPress += (_, e) =>
    {
        e.Cancel = true;
        cts.Cancel();
    };

    consumer.Start(cts.Token);
}
else
{
    Console.WriteLine("Invalid choice.");
}
