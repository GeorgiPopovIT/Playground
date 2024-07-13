using RabbitMQ.Client;
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

        _channel.ExchangeDeclare("orders",ExchangeType.Fanout);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        _channel.BasicPublish(exchange: "", routingKey: "orders", body: body);
    }
}
