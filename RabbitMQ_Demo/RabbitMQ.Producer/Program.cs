using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.ExchangeDeclareAsync(
    exchange: "messages",
    durable: true,
    autoDelete: false,
    type: ExchangeType.Fanout);

await Task.Delay(10000);

for (int i = 0; i < 10; i++)
{
    string message = $"{DateTime.UtcNow} - {Guid.CreateVersion7()}";
    var body = Encoding.UTF8.GetBytes(message);

    //await channel.BasicPublishAsync(
    //    exchange: string.Empty,
    //    routingKey: "message",
    //    body: body,
    //    basicProperties: new BasicProperties { Persistent = true},
    //    mandatory: true);

    await channel.BasicPublishAsync(
        exchange: "messages",
        routingKey: string.Empty,
        body: body,
        basicProperties: new BasicProperties { Persistent = true },
        mandatory: true);

    Console.WriteLine($"Sent {message}");

    await Task.Delay(2000);
}