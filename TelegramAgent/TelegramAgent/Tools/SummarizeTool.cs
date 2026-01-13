using Microsoft.Extensions.AI;
using System.ComponentModel;
using System.Text;

namespace YouTubeSummarizer.Tools;

public class SummarizeTool
{
    private readonly IChatClient _chatClient;

    public SummarizeTool(IChatClient chatClient)
    {
        _chatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
    }

    [Description("Create a comprehensive summary from a video transcript.")]
    public async Task<string> CreateSummaryAsync(string transcript)
    {
        if (string.IsNullOrWhiteSpace(transcript))
        {
            return "No transcript provided for summarization.";
        }

        try
        {
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System,"You are an expert at creating comprehensive, well-structured summaries of video content. " +
                                    "Your task is to analyze the provided transcript and create a clear, concise summary that captures " +
                                    "the main topics, key points, and important insights. Format your response with clear sections and bullet points where appropriate." +
                                    "Return the summary in the same language as the transcript provided"),

                new(ChatRole.User,$"Please create a comprehensive summary of the following video transcript:\n\n{transcript}")
            };

            var response = await _chatClient.GetResponseAsync(messages);

            if (response?.Messages?.Count > 0)
            {
                var summary = new StringBuilder();
                foreach (var content in response.Messages)
                {
                    summary.Append(content.Text);
                }

                return summary.ToString();
            }

            return "Unable to generate summary from the provided transcript.";
        }
        catch (Exception ex)
        {
            return $"Error generating summary: {ex.Message}";
        }
    }
}
