using RabbitMQ_Demo.Data;

namespace RabbitMQ_Demo.RabbitMQ;

public interface IMessageProducer
{
    void SendMessage<T>(T message);

    void SendTopic(Order message);

    void PublishAndSubscripe(Order message);
}
