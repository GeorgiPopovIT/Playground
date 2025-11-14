using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

app.MapPost("/bot", async (TelegramBotClient botClient, ChatClient chatClient, Update update, CancellationToken token) =>
{
    try
    {
        var chatId = update?.Message?.Chat?.Id;

        if (update.Type == UpdateType.Message)
        {
            var question = update.Message?.Text;

            if (string.Equals(update.Message?.Text.Trim(), "/start", StringComparison.OrdinalIgnoreCase))
            {
                await botClient.SendMessage(chatId, "БГ: Напишете вашите изисквания за работа \nEN: Please write your job requirements:");
                return;
            }

            var chatUpdates = chatClient.CompleteChatStreamingAsync(
            [
                    new SystemChatMessage("Jobs from https://www.jobs.bg/ and from https://dev.bg/"),
                new SystemChatMessage("Jobs to be displayed in similar format: Job Title - Job Requirements - Link to the current job"),
                new SystemChatMessage("Ensure links are not expired (no 404 errors)"),
                new SystemChatMessage("Examples of valid job urls: https://www.jobs.bg/job/7931017, https://www.jobs.bg/job/7913796, https://www.jobs.bg/job/7943962, https://www.jobs.bg/job/7942987"),
            new UserChatMessage(question),
        ]);

            StringBuilder sb = new();

            await foreach (var chatUpdate in chatUpdates)
            {
                foreach (var contentPart in chatUpdate.ContentUpdate)
                {
                    sb.Append(contentPart.Text);
                }
            }

            await botClient.SendMessage(chatId, sb.ToString());
        }
    }
    catch (Exception)
    {
        await botClient.SendMessage(update?.Message?.Chat.Id, $"Request failed. Please ask again");
    }
});

app.Run();