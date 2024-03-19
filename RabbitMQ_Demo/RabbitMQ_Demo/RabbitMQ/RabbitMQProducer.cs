using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQ_Demo.RabbitMQ;

public class RabbitMQProducer : IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("orders");

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
    }
}
