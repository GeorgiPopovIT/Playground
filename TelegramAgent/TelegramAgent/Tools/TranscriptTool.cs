using System.ComponentModel;
using System.Text;
using YoutubeExplode;

namespace YouTubeSummarizer.Tools;

internal class TranscriptTool(YoutubeClient youtubeClient)
{
    private readonly YoutubeClient _youtubeClient = youtubeClient;

    [Description("Get the transcript of a YouTube video given its URL.")]
    public async Task<string> GetTranscriptAsync(string url)
    {
        var video = await this._youtubeClient.Videos.GetAsync(url);

        if (video is null)
        {
            return "Invalid YouTube URL.";
        }

        var tracks = await _youtubeClient.Videos.ClosedCaptions.GetManifestAsync(video.Id);

        var track = tracks.Tracks.FirstOrDefault();

        if (track == null)
        {
            return "No transriptions.";
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
