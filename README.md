<!-- GitHub Changes Tracking: All modifications to this repository are tracked via GitHub. Please refer to the commit history for detailed changes. -->

# RAG Chatbot Solution

Welcome to the RAG Chatbot solution repository. This repository contains multiple projects designed to work together to deliver a flexible chatbot experience across different platforms.

## Overview

This solution is organized into three main projects:
- **RAGChatbot.API**: The backend engine that implements chatbot functionality. It uses Azure OpenAI for generating responses and Azure Search to enrich user prompts with context, managing complex conversation flows via API endpoints.
- **RAGChatBot.WebApp**: A web-based frontend built with Blazor and MudBlazor. It provides a responsive user interface for interacting with the chatbot in real time.
- **RAGChatbot.TrayApp**: A Windows tray application built with WPF. It provides quick access to a mini-chat interface via a system tray icon and hotkeys, loading the mini-chat view using WebView2. This allows users to interact with the chatbot without switching to a browser.

The projects are designed to interoperate seamlessly:
- The API provides endpoints consumed by both the web application and the tray application.
- The TrayApp leverages the WebView2 control to load chat interfaces (such as a pared-down version of the WebApp) and supports features like global hotkey toggling (Ctrl+M) for quick access.

## Projects

### RAGChatbot.API: Chatbot Engine
- **Purpose**: Implements the core logic for processing user input and generating contextually relevant responses by integrating with external services like Azure OpenAI and Azure Search.
- **Highlights**:
  - Context enrichment via Azure Search for tailored responses.
  - AI-powered response generation using Azure OpenAI.
  - In-memory management of conversation histories.
- **Directory**: `./RAGChatbot.API`
- **Getting Started**:
  1. Navigate to the `RAGChatbot.API` directory.
  2. Install dependencies (e.g., `dotnet restore`).
  3. Run the application using `dotnet run`.

### RAGChatBot.WebApp: User Interface
- **Purpose**: Offers a web-based UI for interacting with the chatbot with a modern look and real-time updates.
- **Highlights**:
  - Built with Blazor and MudBlazor for responsiveness and simplicity.
  - Consumes API endpoints to display conversations and process new messages.
- **Directory**: `./RAGChatBot.WebApp`
- **Getting Started**:
  1. Navigate to the `RAGChatBot.WebApp` directory.
  2. Install dependencies (e.g., `dotnet restore`).
  3. Start the application using `dotnet run`.

### RAGChatbot.TrayApp: Quick Access Tray Application
- **Purpose**: Provides a lightweight, always-accessible chat interface through the system tray.
- **Highlights**:
  - Built with WPF, featuring a system tray icon and global hotkey support (Ctrl+M) to toggle the chat window.
  - Uses WebView2 to load a mini-chat interface, allowing quick interactions without opening a full browser.
- **Directory**: `./RAGChatbot.TrayApp`
- **Getting Started**:
  1. Navigate to the `RAGChatbot.TrayApp` directory.
  2. Install dependencies (e.g., `dotnet restore`).
  3. Run the application using `dotnet run`.

## Installation and Setup

1. **Clone the Repository**:
   ```
   git clone https://github.com/dorofino/RAGChatbot.git
   cd RAGChatbot
   ```

2. **Setup RAGChatbot.API**:
   - Follow the instructions in `./RAGChatbot.API/README.md`.

3. **Setup RAGChatBot.WebApp**:
   - Follow the instructions in `./RAGChatBot.WebApp/README.md`.

4. **Setup RAGChatbot.TrayApp**:
   - Follow the instructions in `./RAGChatbot.TrayApp/README.md`.

## Usage

After setting up all projects, run them concurrently or as needed:
- **Backend (RAGChatbot.API)**: Ensure it is running to handle chatbot logic and interactions.
- **Frontend (RAGChatBot.WebApp)**: Launch the web interface for full-featured chatbot interactions.
- **Tray Application (RAGChatbot.TrayApp)**: Use the tray application for quick access to a mini-chat window. Use the global hotkey (Ctrl+M) or double-click the tray icon to toggle its visibility.

## Contributing

We welcome contributions to enhance the RAG Chatbot solution. To contribute:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes.
4. Open a pull request detailing your improvements.

## License

Distributed under the MIT License. See `LICENSE` for more information.

## Acknowledgments

- Special thanks to the contributors of each project.
- Inspired by current trends in AI, conversational interfaces, and multi-platform integration.
