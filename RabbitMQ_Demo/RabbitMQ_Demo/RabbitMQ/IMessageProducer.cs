namespace RabbitMQ_Demo.RabbitMQ;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}
