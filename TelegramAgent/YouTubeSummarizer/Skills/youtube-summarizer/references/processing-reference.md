# YouTube Video Processing Reference

## Supported URL Formats

- Standard YouTube URLs: `https://www.youtube.com/watch?v=VIDEO_ID`
- Short URLs: `https://youtu.be/VIDEO_ID`
- Embedded URLs: `https://www.youtube.com/embed/VIDEO_ID`
- Playlist URLs: `https://www.youtube.com/watch?v=VIDEO_ID&list=PLAYLIST_ID`

## Language Support

### Primary Language Priority
- English (`en`) - Always attempted first
- Automatic captions when manual captions unavailable

### Fallback Language Handling
- First available language from video's caption track manifest
- Original transcript language maintained in summary output

## Error Response Reference

### Standard Error Messages
- `"Invalid YouTube URL."` - URL format validation failed
- `"No transcriptions."` - No captions/transcripts available for video
- `"No transcript provided for summarization."` - Empty or null transcript passed to summary tool
- `"Unable to generate summary from the provided transcript."` - AI summarization process failed
- `"Error generating summary: [details]"` - Unexpected exception with specific error information

### Common Failure Scenarios
- **Private Videos**: Return "No transcriptions" (access denied)
- **Age-Restricted Content**: May return "No transcriptions" depending on restrictions
- **Live Streams**: Captions may not be available during live broadcast
- **Very Short Videos**: May have minimal transcript content for meaningful summary

## Performance Expectations

| Video Length | Typical Processing Time |
|-------------|------------------------|
| 0-5 minutes | 10-20 seconds |
| 5-15 minutes | 30-45 seconds |
| 15-30 minutes | 45-90 seconds |
| 30+ minutes | 1-2 minutes |

*Times may vary based on network conditions and AI service response times*