using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Hosting;
using Microsoft.Agents.Hosting.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using System.ClientModel;
using Telegram.Bot;
using Telegram.Bot.Types;
using YoutubeExplode;
using YouTubeSummarizer;
using YouTubeSummarizer.Tools;

var builder = WebApplication.CreateBuilder(args);

var webhookUrl = builder.Configuration["BotWebhookUrl"];
var token = builder.Configuration["TelegramToken"]!;
var endpointUri = builder.Configuration["AZURE_OPENAI_ENDPOINT"];
var deploymentName = builder.Configuration["DEPLOYMENT_NAME"];
var azureOpenAiKey = builder.Configuration["AZURE_OPENAI_KEY"];

IChatClient chatClient = new AzureOpenAIClient(
        new Uri(endpointUri),
        new ApiKeyCredential(azureOpenAiKey))
    .GetChatClient(deploymentName)
    .AsIChatClient();

//builder.WebHost.UseKestrelHttpsConfiguration();
builder.Services.AddOpenApi();

// Add Application Insights
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddHttpClient("tgwebhook")
    .RemoveAllLoggers().AddTypedClient(httpClient =>
        new TelegramBotClient(token, httpClient));

builder.Services.AddHttpClient();

builder.Services.AddSingleton<YoutubeClient>(serviceProvider =>
{
    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient();
    return new YoutubeClient(httpClient);
});
builder.Services.AddSingleton(chatClient);
builder.Services.AddSingleton<TranscriptTool>();
builder.Services.AddSingleton<SummarizeTool>();

builder.AddAIAgent("Summary", (sp, key) =>
{
    var chatClient = sp.GetRequiredService<IChatClient>();
    var transcriptTool = sp.GetRequiredService<TranscriptTool>();
    var summaryTool = sp.GetRequiredService<SummarizeTool>();

    return new ChatClientAgent(
        chatClient,
        name: key,
        instructions: Constants.AIAgentInstructions,
        tools: [AIFunctionFactory.Create(transcriptTool.GetTranscriptAsync),
        AIFunctionFactory.Create(summaryTool.CreateSummaryAsync)]);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGet("/bot/setWebhook", async (TelegramBotClient bot) =>
{
    await bot.SetWebhook(webhookUrl);
    return $"Webhook set to {webhookUrl}";
});
app.MapPost("/bot", async ([FromKeyedServices("Summary")] AIAgent agent, TelegramBotClient bot, Update update, ILogger<Program> logger) =>
{
    var message = update.Message?.Text;
    var chatId = update.Message?.Chat?.Id;

    logger.LogInformation("Received message from chat {ChatId}: {Message}", chatId, message);

    try
    {
        if (string.Equals(message?.Trim(), "/start", StringComparison.OrdinalIgnoreCase))
        {
            await bot.SendMessage(chatId, Constants.WelcomeMessage);
            return;
        }

        await bot.SendMessage(chatId, Constants.ProcessingMessage);

        var result = await agent.RunAsync(message);
        logger.LogInformation("AI Agent response for chat {ChatId}: {Result}", chatId, result.Text);
        
        await bot.SendMessage(chatId, result.Text);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error processing message from chat {ChatId}: {ErrorMessage}", chatId, ex.Message);
        await bot.SendMessage(chatId, $"Request failed. Please ask again");
    }
});

app.Run();