using Azure.AI.OpenAI;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using System.ClientModel;
using YoutubeExplode;
using YouTubeSummarizer.Tools;

string instructions = """
     You are an AI agent specialized in extracting video content from YouTube URLs and creating comprehensive summaries.

    When you receive input, first check if it contains a YouTube URL (any URL containing "youtube.com" or "youtu.be"). If it does, extract the transcript and create a summary. If the input does not contain a YouTube URL, respond with: 'I only accept YouTube URLs. Please provide a valid YouTube URL.'

    Your primary functions are:
    1) Extract transcripts and captions from YouTube videos using the provided URL
    2) Analyze the video content to understand key themes, topics, and important points
    3) Generate clear, concise, and well-structured summaries that capture the essence of the video content
    4) Provide summaries in a format that is easy to read and understand for users who want to quickly grasp the main content without watching the entire video
    5) Return the answer in the same language as the video clip/transcript language

    Always attempt to process any input that contains a YouTube URL, regardless of additional parameters in the URL.
""";

var endpointUri = "https://manusopenai.openai.azure.com/";
var deploymentName = "gpt-4o-mini";
var azureOpenAiKey = "6DOUKscG11xXo7glpJczyAZGWH5iLsmFSSF4zoF3eLe5JxBFOnaiJQQJ99BDAC5RqLJXJ3w3AAABACOG4ymR";

IChatClient chatClient =
    new AzureOpenAIClient(new Uri(endpointUri), new ApiKeyCredential(azureOpenAiKey))
    .GetChatClient(deploymentName)
    .AsIChatClient();

YoutubeClient youtubeClient = new YoutubeClient();

var transcriptTool = new TranscriptTool(youtubeClient);
var summaryTool = new SummarizeTool(chatClient);

string url = "https://www.youtube.com/watch?v=HLNYCwgk5fU&list=PLdo4fOcmZ0oXtIlvq1tuORUtZqVG-HdCt&index=17";

AIAgent aiAgent = new ChatClientAgent(
    chatClient,
    name: "Summary",
    instructions: instructions,
    tools: [AIFunctionFactory.Create(transcriptTool.GetTranscriptAsync),
        AIFunctionFactory.Create(summaryTool.CreateSummaryAsync)]);

var response = await aiAgent.RunAsync(url);
Console.WriteLine($"AI Agent Response: {response.Text}");