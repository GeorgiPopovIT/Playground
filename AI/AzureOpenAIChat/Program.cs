using Azure.AI.OpenAI;
using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Chat;


var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string? endpoint = config["AZURE_OPENAI_ENDPOINT"];
string? deployment = config["AZURE_OPENAI_GPT_NAME"];
string? key = config["AZURE_OPENAI_KEY"];


AzureOpenAIClient azureClient = new(
    new Uri(endpoint),
    new AzureKeyCredential(key));

// This must match the custom deployment name you chose for your model
ChatClient chatClient = azureClient.GetChatClient(deployment);

Console.Write("Ask a question: ");

while (true)
{
    var question = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(question))
    {
        Console.WriteLine("Exiting...");
        return;
    }

    var chatUpdates = chatClient.CompleteChatStreamingAsync(
    [
        new SystemChatMessage("You are a helpful assistant that talks like a pirate."),
        new UserChatMessage(question),
    ]);

    await foreach (var chatUpdate in chatUpdates)
    {
        if (chatUpdate.Role.HasValue)
        {
            Console.Write($"{chatUpdate.Role} : ");
        }

        foreach (var contentPart in chatUpdate.ContentUpdate)
        {
            Console.Write(contentPart.Text);
        }
    }

    Console.Write("Ask another question: ");
}


