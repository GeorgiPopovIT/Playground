using Confluent.Kafka;

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9093",
    GroupId = "dotnet-consumer-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("test-topic");

try
{
    while (true)
    {
        var result = consumer.Consume();
        Console.WriteLine($"Consumed: '{result.Message.Value}' from partition {result.Partition}");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Consumer stopped.");
}
finally
{
    consumer.Close();
}