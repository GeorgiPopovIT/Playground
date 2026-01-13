using System.ComponentModel;
using System.Text;
using YoutubeExplode;

namespace YouTubeSummarizer.Tools;

public sealed class TranscriptTool(YoutubeClient youtubeClient, ILogger<TranscriptTool> logger)
{
    private readonly YoutubeClient _youtubeClient = youtubeClient
        ?? throw new ArgumentNullException(nameof(youtubeClient));
    private readonly ILogger<TranscriptTool> _logger = logger
        ?? throw new ArgumentNullException(nameof(logger));

    [Description(Constants.TranscriptToolDescription)]
    public async Task<string> GetTranscriptAsync(string url)
    {

        _logger.LogDebug("Url: {url}", url);
        var video = await _youtubeClient.Videos.GetAsync(url);
        _logger.LogDebug("VideoObj: {VideoObj}", video.Id);
        if (video is null)
        {
            return Constants.InvalidYouTubeUrlMessage;
        }



        var tracks = await _youtubeClient.Videos.ClosedCaptions.GetManifestAsync(video.Id);

        var track = tracks.GetByLanguage(Constants.EnglishLanguageCode)
                   ?? tracks.Tracks.FirstOrDefault();

        if (track == null)
        {
            return Constants.NoTranscriptionsMessage;
        }

        var closedCaptions = await _youtubeClient.Videos.ClosedCaptions.GetAsync(track);

        var stringBuilder = new StringBuilder();
        foreach (var caption in closedCaptions.Captions)
        {
            if (stringBuilder.Length > 0)
            {
                stringBuilder.Append(' ');
            }
            stringBuilder.Append(caption.Text);
        }

        return stringBuilder.ToString();
    }
}
