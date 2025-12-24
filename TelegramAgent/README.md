# YouTube Video Summarizer Agent

A sophisticated AI-powered agent that extracts transcripts from YouTube videos and creates comprehensive summaries using Azure OpenAI services.

## 🎯 What is this Agent?

The YouTube Video Summarizer Agent is an intelligent system that:

- **Accepts YouTube URLs** as input from users
- **Extracts video transcripts** using closed captions and subtitles
- **Generates comprehensive summaries** using Azure OpenAI's GPT models
- **Supports multiple deployment options** including console application and Telegram bot
- **Provides multilingual support** - returns summaries in the same language as the video content

### Key Features

- ✅ **Smart URL Detection** - Recognizes valid YouTube URLs with various parameters
- ✅ **Transcript Extraction** - Leverages YoutubeExplode library for reliable transcript extraction
- ✅ **AI-Powered Summaries** - Uses Azure OpenAI for intelligent content analysis
- ✅ **Multiple Interfaces** - Console app and Telegram bot implementations
- ✅ **Error Handling** - Robust error handling for various edge cases
- ✅ **Logging & Monitoring** - Application Insights integration for telemetry

## 🛠️ Frameworks & Technologies

### Core Frameworks

| Framework/Library | Version | Purpose |
|-------------------|---------|---------|
| **.NET 10** | Latest | Runtime platform |
| **ASP.NET Core** | 10.0 | Web application framework (for Telegram bot) |
| **Microsoft.Agents.AI** | Latest | AI agent framework for building intelligent agents |

### AI & Machine Learning

| Service/Library | Purpose |
|-----------------|---------|
| **Azure OpenAI** | GPT model integration for text summarization |
| **Microsoft.Extensions.AI** | AI abstractions and integrations |

### YouTube Integration

| Library | Purpose |
|---------|---------|
| **YoutubeExplode** | YouTube video metadata and transcript extraction |

### Communication & Messaging

| Library | Purpose |
|---------|---------|
| **Telegram.Bot** | Telegram Bot API integration |
| **Microsoft.AspNetCore.OpenApi** | API documentation and testing |

## 🏗️ Architecture

### Project Structure

```
TelegramAgent/
├── YouTubeSummarizer/          # Main web application (Telegram bot)
│   ├── Tools/
│   │   ├── TranscriptTool.cs   # YouTube transcript extraction
│   │   └── SummarizeTool.cs    # AI summarization logic
│   ├── Constants.cs            # Application constants
│   └── Program.cs              # Web app configuration
├── TelegramAgent/              # Console application
│   ├── Tools/
│   │   ├── TranscriptTool.cs   # YouTube transcript extraction
│   │   └── SummarizeTool.cs    # AI summarization logic
│   └── Program.cs              # Console app entry point
└── README.md                   # This file
```

### Agent Architecture

The agent follows a **tool-based architecture** where:

1. **TranscriptTool** - Handles YouTube URL processing and transcript extraction
2. **SummarizeTool** - Manages AI-powered content summarization
3. **ChatClientAgent** - Orchestrates tool execution and user interactions
4. **Dependency Injection** - Manages service lifecycles and dependencies

## 🚀 Deployment Options

### 1. Console Application (`TelegramAgent`)
- Direct command-line interface
- Perfect for testing and development
- Immediate response display

### 2. Telegram Bot (`YouTubeSummarizer`)
- Web-based ASP.NET Core application
- Webhook-based Telegram integration
- Scalable production deployment
- Application Insights monitoring

## ⚙️ Configuration

### Required Environment Variables

## 🔧 How to Run

### Console Application
```bash
cd TelegramAgent
dotnet run
```

### Telegram Bot
```bash
cd YouTubeSummarizer
dotnet run
```

## 🎮 Usage Examples

### Input
```
https://www.youtube.com/watch?v=HLNYCwgk5fU&list=PLdo4fOcmZ0oXtIlvq1tuORUtZqVG-HdCt&index=17
```

### Output
The agent will:
1. Validate the YouTube URL
2. Extract the video transcript
3. Generate a comprehensive summary
4. Return the summary in the video's original language
## 📦 Dependencies

See individual `*.csproj` files for complete dependency lists. Key NuGet packages:
- `Microsoft.Agents.AI.Hosting`
- `Azure.AI.OpenAI`
- `YoutubeExplode`
- `Telegram.Bot`
- `Microsoft.ApplicationInsights.AspNetCore`

---

**Built with ❤️ using Microsoft AI Agent Framework and Azure OpenAI**