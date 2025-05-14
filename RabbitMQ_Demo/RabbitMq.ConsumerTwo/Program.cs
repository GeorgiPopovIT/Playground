using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Consumer Two");

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(
    exchange: "messages",
    durable: true,
    autoDelete: false,
    type: ExchangeType.Fanout);

await channel.QueueDeclareAsync(
    queue: "message-2",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null);


await channel.QueueBindAsync(queue: "message-2", exchange: "messages", routingKey: string.Empty);

Console.WriteLine("Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (sender, eventArgs) =>
{
    var body = eventArgs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received {message}");

    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, false);
};

await channel.BasicConsumeAsync("message-2", autoAck: false, consumer: consumer);

Console.ReadLine();