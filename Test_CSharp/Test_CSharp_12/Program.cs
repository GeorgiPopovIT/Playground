using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
serviceCollection.AddEasyNetQ("host=localhost;timeout=60").UseSystemTextJson();

using var provider = serviceCollection.BuildServiceProvider();
var bus = provider.GetRequiredService<IBus>();

var input = string.Empty;
Console.WriteLine("Enter a message. 'Quit' to quit.");
while ((input = Console.ReadLine()) != "Quit")
{
    await bus.PubSub.PublishAsync(new TextMessage { Text = input });
    Console.WriteLine("Message published!");
}

public class TextMessage
{
    public string Text { get; set; }
}