---
name: youtube-summarizer
description: Extracts transcripts from YouTube videos and creates comprehensive summaries. Use when users provide YouTube URLs or request video content analysis, summaries, or transcriptions. Don't use for non-YouTube video platforms like Vimeo, TikTok, or local video files.
---

# YouTube Video Summarizer Skill

Extract transcripts from YouTube videos and generate comprehensive summaries using specialized tools and AI analysis.

## Core Workflow

Execute these steps in sequence when processing YouTube video requests:

### Step 1: Validate YouTube URL
- Check that the provided URL contains `youtube.com/watch` or `youtu.be/`
- If invalid URL detected, return: "Invalid YouTube URL."

### Step 2: Extract Video Transcript
- Use the YoutubeClient to retrieve video metadata and closed captions
- Prioritize English (`en`) language transcripts when available
- Fall back to first available language if English unavailable
- If no transcripts available, return: "No transcriptions."

### Step 3: Process Transcript Content
- Clean and format the extracted captions text
- Remove timestamps and formatting artifacts
- Concatenate caption segments into continuous text

### Step 4: Generate Summary
- Use the AI chat client to analyze transcript content
- Apply the system prompt for comprehensive summary generation
- Return summary in the same language as source transcript
- If summarization fails, return: "Unable to generate summary from the provided transcript."

## Implementation Notes

- Response time scales with video length (30-minute video ≈ 1 minute processing)
- Handle all YouTube URL formats (long-form, short-form, embedded)
- Maintain original transcript language in final summary
- Use structured formatting with sections and bullet points in summaries

## Error Handling

Return these specific error messages for common failure scenarios:

- **Invalid YouTube URL**: "Invalid YouTube URL."
- **No captions available**: "No transcriptions."  
- **Empty transcript**: "No transcript provided for summarization."
- **AI processing failure**: "Unable to generate summary from the provided transcript."
- **Unexpected errors**: "Error generating summary: [error details]"

For comprehensive error handling details and performance expectations, see `references/processing-reference.md`.

## Summary Formatting

Structure summaries with clear sections and bullet points. For detailed formatting guidelines and template structure, see `assets/summary-template.md`.

**Input**: "https://www.youtube.com/watch?v=VIDEO_ID"
**Output**: Comprehensive summary with key points, main topics, and insights from the video

**Input**: YouTube URL with additional parameters
**Output**: Process the core video content and provide summary regardless of URL parameters