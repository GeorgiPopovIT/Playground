using RabbitMQ.Client;
using RabbitMQ_Demo.Data;
using System.Text;
using System.Text.Json;

namespace RabbitMQ_Demo.RabbitMQ;

public class RabbitMQProducer : IMessageProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public RabbitMQProducer()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost", // RabbitMQ server address
            Port = 5672,            // Default port
            UserName = "guest",     // Default username
            Password = "guest"      // Default password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }


    public void SendMessage<T>(T message)
    {

        //using var connection = factory.CreateConnection();
        //using var channel = connection.CreateModel();

        _channel.ExchangeDeclare("orders", ExchangeType.Fanout);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        _channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
    }
    public void SendTopic(Order message)
    {
        _channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

        var routingKey = "message.info";
        var result = $"{message.ProductName} - {message.Quantity}";
        var body = Encoding.UTF8.GetBytes(result);
        _channel.BasicPublish(exchange: "topic_logs",
                             routingKey: routingKey,
                             basicProperties: null,
                             body: body);

    }

   public void PublishAndSubscripe(Order message)
    {
        _channel.ExchangeDeclare("logs", ExchangeType.Fanout);

        var body = Encoding.UTF8.GetBytes(message.ProductName);
        _channel.BasicPublish(exchange: "logs",
                     routingKey: string.Empty,
                     basicProperties: null,
                     body: body);
    }
}
