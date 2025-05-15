using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(_ =>
{
    var credentials = new AzureKeyCredential(builder.Configuration["Azure:OpenAiKey"]!);
    var client = new AzureOpenAIClient(new Uri(builder.Configuration["Azure:OpenAiEndpoint"]!), credentials);
    return client;
});

// Register ChatClient abstraction
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<AzureOpenAIClient>();
    return client.GetChatClient(builder.Configuration["Azure:DeploymentModel"]!);
});

builder.Services.AddHttpClient("tgwebhook")
    .RemoveAllLoggers()
    .AddTypedClient(httpClient => new TelegramBotClient(builder.Configuration["TelegramToken"]!, httpClient));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.MapGet("/bot/setWebhook", async (TelegramBotClient bot) =>
{
    await bot.SetWebhook(builder.Configuration["BotWebhookUrl"]!);
    return $"Webhook set to {builder.Configuration["BotWebhookUrl"]!}";
});

app.MapPost("/bot", async(TelegramBotClient botClient, ChatClient chatClient, Update update, CancellationToken token) =>
{
    var chatId = update?.Message?.Chat?.Id;

    var question = update?.Message?.Text;

    if (string.IsNullOrWhiteSpace(question))
    {
        await botClient.SendMessage(chatId, "Please add you jobs requiremets:", cancellationToken: token);
        return;
    }

    if (update?.Message is not null && update?.Message?.Text is "/start")
    {
        await botClient.SendMessage(chatId, "Please add you jobs requiremets:", cancellationToken: token);
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

    await botClient.SendMessage(chatId, sb.ToString(), cancellationToken: token);
});

app.Run();