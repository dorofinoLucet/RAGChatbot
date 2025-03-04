# RAGChatBot.WebApp

## Overview
RAGChatBot.WebApp is a web application that provides a user interface for interacting with the RAGChatbot API. It is built using Blazor and MudBlazor for a modern, responsive UI.

## Features
- Real-time chat interface
- Conversation management
- Integration with RAGChatbot API for enhanced chatbot responses

## Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download)
- Azure subscription (for Azure OpenAI and other services)
- RAGChatbot.API (see [RAGChatbot.API README](../RAGChatbot.API/README.md) for setup instructions)

## Installation
1. Clone this repository.
    ```sh
    git clone https://github.com/dorofino/RAGChatBot.WebApp.git
    ```
2. Navigate to the project folder.
    ```sh
    cd RAGChatBot.WebApp
    ```
3. Build and run the project.
    ```sh
    dotnet build
    dotnet run
    ```

## Configuration
Update `appsettings.json` with your API keys and endpoints. Values may include:
- `RAGChatbotAPIUrl`

Example `appsettings.json`:
```json
{
  "RAGChatbotAPIUrl": "https://localhost:7066/api/Chatbot"
}
```

If you do not have Azure services set up, visit:
- [Create an Azure OpenAI Resource](https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource)
- [Azure Storage Account](https://azure.microsoft.com/services/storage/)

## Usage
Once running, navigate to `https://localhost:5001` in your browser. The application provides the following features:

### Chat Interface
- Start a new conversation by clicking the "New Conversation" button.
- Type your message in the input field and press Enter to send.
- View the conversation history in the chat window.

### Conversation Management
- View a list of all conversations in the sidebar.
- Click on a conversation to view its details.
- Delete a conversation by clicking the delete icon next to it.

## Integration with RAGChatbot.API
RAGChatBot.WebApp interacts with RAGChatbot.API to provide enhanced chatbot responses. The API endpoints used include:

### POST /api/Chat
Handles chat requests. Accepts a `ChatRequest` object and an optional `conversationId`. If the `conversationId` is not provided or not found, a new conversation is created. The prompt is enriched with context from Azure Search and the response from the chat service is returned.
```json
{
  "message": "Hello, chatbot!"
}
```
Response:
```json
{
  "conversationId": "generated-conversation-id",
  "response": "Chatbot reply"
}
```

### GET /api/Chat/conversations
Returns a summary of all conversations. Each summary includes the conversation ID and the title.
```json
[
  {
    "ConversationId": "conversation-id",
    "Title": "Conversation title"
  }
]
```

### GET /api/Chat/conversations/{conversationId}
Returns the details of a specific conversation by its ID. The details include the conversation ID, title, and messages.
```json
{
  "ConversationId": "conversation-id",
  "Title": "Conversation title",
  "Messages": [
    {
      "Sender": "You",
      "Text": "User message"
    },
    {
      "Sender": "Bot",
      "Text": "Bot reply"
    }
  ]
}
```

### DELETE /api/Chat/conversations/{conversationId}
Deletes a specific conversation by its ID. If the conversation is found and deleted, a success message is returned. Otherwise, a not found message is returned.
```json
{
  "message": "Conversation deleted."
}
```

## Contributing
Feel free to submit pull requests or open issues for improvements.

## License
This project is licensed under the MIT License.