# RAGChatbot.TrayApp

RAGChatbot.TrayApp is a Windows tray application that provides quick access to a mini-chat interface. It is part of the RAG Chatbot solution and integrates seamlessly with the backend API and web application. This project allows users to interact with the chatbot directly from the system tray, offering a lightweight and always-accessible chat interface.

## Overview

- **Purpose**: Provides a lightweight, always-accessible chat interface through the system tray using a mini-chat view loaded with WebView2.
- **Key Features**:
  - System tray icon with double-click support.
  - Global hotkey (Ctrl+M) to toggle the visibility of the chat window.
  - WebView2 integration to load a mini-chat interface from the RAGChatBot.WebApp.
  - User-friendly interactions without the need to open a full browser.

## Getting Started

1. **Clone the Repository**:
   ```
   git clone https://github.com/dorofino/RAGChatbot
   cd RAGChatbot
   ```

2. **Setup the Tray Application**:
   - Navigate to the `RAGChatbot.TrayApp` directory.
   - Restore dependencies (e.g., using `dotnet restore`).
   - Run the application with `dotnet run`.

## Usage

- **Launching the Tray Application**: On running the application, the tray app starts and displays a mini-chat window.
- **Interacting with the App**:
  - Use the **global hotkey (Ctrl+M)** to toggle the visibility of the chat window.
  - Alternatively, double-click the tray icon to show or hide the chat interface.
  - The chat interface loads a mini version of the chatbot, allowing for quick interactions.

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Commit your changes.
4. Open a pull request with a detailed description of your improvements.

## License

This project is distributed under the MIT License. See the `LICENSE` file for more details.

## Acknowledgments

- Thank you to all contributors for their efforts.
- Inspired by modern trends in desktop applications and interactive chatbot interfaces.
