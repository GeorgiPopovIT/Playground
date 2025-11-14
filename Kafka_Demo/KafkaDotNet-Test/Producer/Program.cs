using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9093",  // Use 9093 for host access
    MessageTimeoutMs = 5000
};
using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    for (int i = 0; i < 10; i++)
    {
        var message = $"Message {i}";
        var result = await producer.ProduceAsync(
            "test-topic",
            new Message<Null, string> { Value = message }
        );
        Console.WriteLine($"Produced: '{message}' to partition {result.Partition}");
    }
}
catch (ProduceException<Null, string> ex)
{
    Console.WriteLine($"Delivery failed: {ex.Error.Reason}");
}
