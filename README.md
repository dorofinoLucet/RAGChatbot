# RAG Chatbot Solution

Welcome to the RAG Chatbot solution repository. This repository contains two distinct projects designed to work in tandem to deliver an advanced chatbot experience.

## Overview

This solution is organized into two main projects:
- **RAGChatbot.API**: Responsible for the chatbot's core functionality, including natural language processing (NLP), response generation, and integration with external APIs.
- **RAGChatBot.WebApp**: Focused on the front-end interface and user interaction, providing a seamless experience and powerful visualizations.

Both projects are built to be modular and scalable, ensuring high performance and ease of maintenance.

## Projects

### RAGChatbot.API: Chatbot Engine
- **Purpose**: Implements the core logic, processing user input and generating contextually relevant responses.
- **Highlights**:
  - Advanced NLP algorithms and response ranking using Azure OpenAI.
  - Integration with Azure Search for enriched data.
  - Scalable and modular architecture.
- **Directory**: `./RAGChatbot.API`
- **Getting Started**:
  1. Navigate to the `RAGChatbot.API` directory.
  2. Install dependencies using your package manager (e.g., `dotnet restore`).
  3. Run the application using `dotnet run`.

### RAGChatBot.WebApp: User Interface
- **Purpose**: Provides a user-friendly interface for interacting with the chatbot, featuring real-time updates and visual analytics.
- **Highlights**:
  - Responsive design for optimal performance on various devices.
  - Integration with the Chatbot Engine to display real-time responses.
  - Clean and modern user interface built with Blazor and MudBlazor.
- **Directory**: `./RAGChatBot.WebApp`
- **Getting Started**:
  1. Navigate to the `RAGChatBot.WebApp` directory.
  2. Install the required dependencies using `dotnet restore`.
  3. Start the front-end server using `dotnet run`.

## Installation and Setup

1. **Clone the Repository**:
   ```
   git clone https://github.com/yourusername/RAGChatbot.git
   cd RAGChatbot
   ```

2. **Setup RAGChatbot.API**:
   - Follow the instructions in the `./RAGChatbot.API/README.md`.

3. **Setup RAGChatBot.WebApp**:
   - Follow the instructions in the `./RAGChatBot.WebApp/README.md`.

## Usage

After setting up both projects, run them concurrently (if required):
- **RAGChatbot.API (Backend)**: Ensure it is running to process and generate responses.
- **RAGChatBot.WebApp (Frontend)**: Launch the interface to interact with the chatbot.

Integration between the projects is handled via API endpoints defined in RAGChatbot.API and consumed by RAGChatBot.WebApp.

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
- Inspired by current trends in AI and conversational interfaces.