using System.ComponentModel;
using System.Text;
using Microsoft.Extensions.AI;

namespace YouTubeSummarizer.Tools;

public sealed class SummarizeTool(IChatClient chatClient)
{
    private readonly IChatClient _chatClient = chatClient
        ?? throw new ArgumentNullException(nameof(chatClient));

    [Description(Constants.SummarizeToolDescription)]
    public async Task<string> CreateSummaryAsync(string transcript)
    {
        if (string.IsNullOrWhiteSpace(transcript))
        {
            return Constants.NoTranscriptProvidedMessage;
        }

        try
        {
            var messages = new List<ChatMessage>
            {
                new(ChatRole.System, Constants.SystemSummaryPrompt),
                new(ChatRole.User, string.Format(Constants.UserSummaryPromptTemplate, transcript))
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

            return Constants.UnableToGenerateSummaryMessage;
        }
        catch (Exception ex)
        {
            return $"{Constants.ErrorGeneratingSummaryPrefix}{ex.Message}";
        }
    }
}
