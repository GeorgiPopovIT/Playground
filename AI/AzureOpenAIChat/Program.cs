using Azure.AI.OpenAI;
using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Chat;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Text;


var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string? endpoint = config["AZURE_OPENAI_ENDPOINT"];
string? deployment = config["AZURE_OPENAI_GPT_NAME"];
string? key = config["AZURE_OPENAI_KEY"];

AzureOpenAIClient azureClient = new(
   new Uri(endpoint),
   new AzureKeyCredential(key));
// This must match the custom deployment name you chose for your model
ChatClient chatClient = azureClient.GetChatClient(deployment);

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("8002117385:AAHtj3fP8XunY7AvRyD5tVxzWy3ybWuJ4dE", cancellationToken: cts.Token);

await bot.DeleteWebhook();          // you may comment this line if you find it unnecessary
await bot.DropPendingUpdates();
Console.Write("Working... ");

bot.OnUpdate += ChatResponse;
// This must match the custom deployment name you chose for your model
Console.ReadKey();

async Task ChatResponse(Update update)
{
    var chatId = update?.Message?.Chat?.Id;

    AzureOpenAIClient azureClient = new(
    new Uri(endpoint),
    new AzureKeyCredential(key));

    // This must match the custom deployment name you chose for your model
    ChatClient chatClient = azureClient.GetChatClient(deployment);

    var question = update?.Message?.Text;

    if (string.IsNullOrWhiteSpace(question))
    {
        await bot.SendMessage(chatId, "Please add you jobs requiremets:");
        return;
    }

    if (update?.Message is not null && update?.Message?.Text is "/start")
    {
        await bot.SendMessage(chatId, "Please add you jobs requiremets:");
        return;
    }

    var chatUpdates = chatClient.CompleteChatStreamingAsync(
    [
            new SystemChatMessage("Jobs from https://www.jobs.bg/ and from https://dev.bg/"),
            new SystemChatMessage("Jobs to be displayed in similar format: Job Title - Job Requirements - Link to the current job"),
            new SystemChatMessage("Jobs to be not expired. When open the link to not be with status code 404. See url addresse to be valid"),
            new SystemChatMessage("Examples of valid job urls: https://www.jobs.bg/job/7931017, https://www.jobs.bg/job/7913796, https://www.jobs.bg/job/7943962, https://www.jobs.bg/job/7942987"),
        new UserChatMessage(question),
    ]);

    StringBuilder sb = new();

    await foreach (var chatUpdate in chatUpdates)
    {
        foreach (var contentPart in chatUpdate.ContentUpdate)
        {
            sb.Append(contentPart);
        }
    }

    await bot.SendMessage(chatId, sb.ToString());
}

