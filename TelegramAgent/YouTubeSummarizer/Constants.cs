namespace YouTubeSummarizer;

internal static class Constants
{
    internal const string InvalidYouTubeUrlMessage = "Invalid YouTube URL.";
    internal const string NoTranscriptionsMessage = "No transriptions.";
    internal const string NoTranscriptProvidedMessage = "No transcript provided for summarization.";
    internal const string UnableToGenerateSummaryMessage = "Unable to generate summary from the provided transcript.";
    internal const string ErrorGeneratingSummaryPrefix = "Error generating summary: ";

    internal const string EnglishLanguageCode = "en";

    internal const string ProcessingMessage = "⏳ Processing your request, please wait...";

    internal const string AIAgentInstructions = """
        You are an AI agent specialized in extracting video content from YouTube URLs and creating comprehensive summaries.

        Your primary functions are:
        1) Extract transcripts and captions from YouTube videos using the provided URL
        2) Analyze the video content to understand key themes, topics, and important points
        3) Generate clear, concise, and well-structured summaries that capture the essence of the video content
        4) Provide summaries in a format that is easy to read and understand for users who want to quickly grasp the main content without watching the entire video
        5) Return the answer in the same language as the video clip/transcript language

        Always attempt to process any input that contains a YouTube URL, regardless of additional parameters in the URL.
    """;

    internal const string SystemSummaryPrompt = """
        You are an expert at creating comprehensive, well-structured summaries of video content.
        Your task is to analyze the provided transcript and create a clear, concise summary that captures
        the main topics, key points, and important insights. Format your response with clear sections and bullet points where appropriate.
        Return the summary in the same language as the transcript provided.
        """;

    internal const string UserSummaryPromptTemplate = "Please create a comprehensive summary of the following video transcript:\n\n{0}";

    internal const string TranscriptToolDescription = "Get the transcript of a YouTube video given its URL.";
    internal const string SummarizeToolDescription = "Create a comprehensive summary from a video transcript.";

    internal const string WelcomeMessage = """
        🎬 Welcome to YouTube Video Summarizer Bot!
        
        📝 Please send me only the URL of a YouTube video that you want to summarize.
        
        ⏱️ Note: Response may take some time. For example, a 30-minute video needs around 1 minute to process.
        
        Example: https://www.youtube.com/watch?v=VIDEO_ID
        """;

    internal const string AIAgentDescription = "AI agent that extracts transcripts from YouTube videos and creates comprehensive summaries";
}
