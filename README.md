# PicLibBot 🤖🔍

A Telegram inline bot that provides instant image search results using the Brave Search API. Simply type `@your_bot_name` followed by your search query in any Telegram chat to get relevant image results instantly!

## ✨ Features

- **Inline Image Search**: Search for images directly within any Telegram conversation
- **Brave Search Integration**: Powered by Brave's image search API for high-quality results
- **Configurable Results**: Customize the maximum number of inline results
- **Docker Support**: Easy deployment with Docker
- **Robust Logging**: Comprehensive logging with NLog
- **Resilient HTTP Client**: Built-in retry policies and error handling
- **Safe Search**: Search with safety filters disabled for comprehensive results

## 🛠️ Technology Stack

- **Runtime**: .NET 9.0
- **Framework**: ASP.NET Core with Hosted Services
- **HTTP Client**: Refit with Polly for resilience
- **Telegram API**: Telegram.Bot library
- **Search API**: Brave Search API
- **Logging**: NLog with structured logging
- **Containerization**: Docker with Linux containers
- **Configuration**: JSON configuration with environment variable support

## 🚀 Quick Start

### Prerequisites

1. **Telegram Bot Token**: Create a bot via [@BotFather](https://t.me/BotFather) on Telegram
2. **Brave Search API Key**: Get your API key from [Brave Search API](https://api.search.brave.com/)

### Configuration

1. Copy the example configuration:
   ```bash
   cp PicLibBot/appsettings.example.json PicLibBot/appsettings.json
   ```

2. Edit `appsettings.json` with your credentials:
   ```json
   {
     "PicLibBot": {
       "TelegramBotApiKey": "YOUR_TELEGRAM_BOT_TOKEN",
       "BraveApiKey": "YOUR_BRAVE_API_KEY",
       "MaxInlineResults": 20
     }
   }
   ```

   Or use environment variables with the `PICLIBBOT_` prefix:
   ```bash
   export PICLIBBOT_PicLibBot__TelegramBotApiKey="YOUR_TELEGRAM_BOT_TOKEN"
   export PICLIBBOT_PicLibBot__BraveApiKey="YOUR_BRAVE_API_KEY"
   export PICLIBBOT_PicLibBot__MaxInlineResults=20
   ```

### Running Locally

```bash
# Build and run
dotnet run --project PicLibBot

# Or build first, then run
dotnet build
dotnet run --project PicLibBot
```

### Running with Docker

```bash
# Build the Docker image
docker build -t piclibbot .

# Run with environment variables
docker run -d \
  -e PICLIBBOT_PicLibBot__TelegramBotApiKey="YOUR_TELEGRAM_BOT_TOKEN" \
  -e PICLIBBOT_PicLibBot__BraveApiKey="YOUR_BRAVE_API_KEY" \
  -e PICLIBBOT_PicLibBot__MaxInlineResults=20 \
  --name piclibbot \
  piclibbot
```

## 📱 Usage

1. **Start a conversation** with your bot or add it to a group
2. **Enable inline mode** by configuring your bot with @BotFather:
   - Send `/setinline` command to @BotFather
   - Select your bot
   - Set a placeholder text (e.g., "Search for images...")
3. **Use inline search** in any chat:
   ```
   @your_bot_name cute cats
   ```
4. **Select an image** from the results to send it

## 🏗️ Project Structure

```
PicLibBot/
├── Abstractions/          # Interfaces and contracts
│   └── IBraveSearch.cs    # Brave Search API interface
├── Enums/                 # Enumeration types
├── Exceptions/            # Custom exception types
├── Extensions/            # Extension methods
├── Models/                # Data models and DTOs
├── Services/              # Business logic services
│   ├── TelegramBotService.cs  # Main bot service
│   └── Worker.cs          # Background worker
├── Properties/            # Assembly properties
├── Program.cs             # Application entry point
├── Dockerfile             # Docker configuration
├── nlog.config           # Logging configuration
└── appsettings.json      # Application configuration
```

## ⚙️ Configuration Options

| Setting | Description | Default | Required |
|---------|-------------|---------|----------|
| `TelegramBotApiKey` | Telegram Bot API token from @BotFather | - | ✅ |
| `BraveApiKey` | Brave Search API subscription token | - | ✅ |
| `MaxInlineResults` | Maximum number of inline results to return | 20 | ❌ |

## 🔧 Development

### Building

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests
dotnet test
```

### Code Quality

The project uses:
- **EditorConfig** for consistent code formatting
- **Nullable reference types** enabled
- **Directory.Build.props** for shared MSBuild properties
- **Directory.Packages.props** for centralized package management

## 🐳 Docker

The project includes a multi-stage Dockerfile optimized for production:

- Uses official .NET runtime images
- Optimized layer caching
- Security best practices
- Minimal attack surface

## 📊 Logging

Comprehensive logging is implemented using NLog with:
- Structured logging
- Multiple log levels
- Configurable output targets
- Request/response logging for HTTP calls
