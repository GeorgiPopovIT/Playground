using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddEasyNetQ("host=localhost;timeout=60").UseSystemTextJson();

using var provider = serviceCollection.BuildServiceProvider();
var bus = provider.GetRequiredService<IBus>();

await bus.PubSub.SubscribeAsync<TextMessage>("test", HandleTextMessage);
Console.WriteLine("Listening for messages. Hit <return> to quit.");
Console.ReadLine();

static void HandleTextMessage(TextMessage textMessage)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Got message: {0}", textMessage.Text);
    Console.ResetColor();
}


public class TextMessage
{
    public string Text { get; set; }
}
