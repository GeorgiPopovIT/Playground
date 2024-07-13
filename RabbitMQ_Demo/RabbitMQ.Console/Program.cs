using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

//GetMessageFromQueue();
//GetMessageFromTopic();
GetMessagePublishSub();

Console.ReadKey();

void GetMessageFromQueue()
{
    channel.QueueDeclare("orders", exclusive: false);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, eventArgs) => {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Orders message received: {message}");
    };

    channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
}

void GetMessageFromTopic()
{
    channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);
    // declare a server-named queue
    var queueName = channel.QueueDeclare().QueueName;

   
        channel.QueueBind(queue: queueName,
                          exchange: "topic_logs",
                          routingKey: "message.info");

        Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine(" Press [enter] to exit.");

}

void GetMessagePublishSub()
{
    channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

    var queueName = channel.QueueDeclare().QueueName;

    channel.QueueBind(queue: queueName,
                  exchange: "logs",
                  routingKey: string.Empty);


    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        byte[] body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] {message}");
    };
    channel.BasicConsume(queue: queueName,
                         autoAck: true,
                         consumer: consumer);
}