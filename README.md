# SmartOrganizer

A C# WPF desktop application that automatically organizes files using AI-powered classification. Intelligently sorts files from Desktop and Downloads folders into organized directories.

## Features

- **AI-Powered Classification** - Uses Mistral AI model via Ollama
- **Automatic Organization** - Creates folders and moves files by type
- **Smart Categories** - Documents, Pictures, Videos, Games, Software, Media
- **Modern UI** - Clean dark theme with smooth animations
- **Batch Processing** - Organize entire folders at once

## Prerequisites

- [Ollama](https://ollama.ai/) installed and running
- Mistral model downloaded: `ollama pull mistral`

## Quick Start

1. **Install Ollama** and download Mistral model
2. **Build & Run**:
   ```bash
   # Open SmartOrganizer.sln in Visual Studio
   # Restore NuGet packages
   # Build and run (F5)
   ```
3. **Use**: Click "Organize Desktop" or "Organize Downloads"

## File Categories

| Category | Description |
|----------|-------------|
| **Documents** | Office files, PDFs, text files |
| **Pictures** | Images (JPG, PNG, GIF, etc.) |
| **Videos** | Video files and media players |
| **Games** | Games and Game launchers |
| **Software** | General utilities and tools |
| **Media** | Media players and audio files |

## Configuration

The application connects to `localhost:11434` by default. Modify `AIClassifier.cs` to change AI model or endpoint settings.

## Requirements

- .NET Framework 4.8
- Windows OS
- Ollama service running on port 11434

## License

MIT License - see [LICENSE](LICENSE) file for details.

---

**SmartOrganizer** - Intelligent file organization powered by AI
