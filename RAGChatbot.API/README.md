# RAGChatbot.API

## Overview
RAGChatbot.API is a RESTful service that provides Retrieval-Augmented Generation (RAG) functionality for conversation-like interactions. It aims to streamline chatbot development with easy configuration options and straightforward deployment steps.

## Features
- Retrieval-Augmented Generation approach to enhance chatbot responses
- Azure OpenAI integration for improved language understanding
- Configurable app settings for different environments

## Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download)
- Azure subscription (for Azure OpenAI and other services)

## Installation
1. Clone this repository.
    ```sh
    git clone https://github.com/yourusername/RAGChatbot.API.git
    ```
2. Navigate to the project folder.
    ```sh
    cd RAGChatbot.API
    ```
3. Build and run the project.
    ```sh
    dotnet build
    dotnet run
    ```

## Configuration
Update `appsettings.json` with your Azure keys and endpoints. Values may include:
- `AzureOpenAIKey`
- `AzureOpenAIEndpoint`
- `AzureStorageConnectionString`

Example `appsettings.json`:
```json
{
  "AzureOpenAIKey": "your-azure-openai-key",
  "AzureOpenAIEndpoint": "https://your-openai-endpoint",
  "AzureStorageConnectionString": "your-azure-storage-connection-string"
}
```

If you do not have Azure services set up, visit:
- [Create an Azure OpenAI Resource](https://learn.microsoft.com/azure/cognitive-services/openai/how-to/create-resource)
- [Azure Storage Account](https://azure.microsoft.com/services/storage/)

## Usage
Once running, send HTTP requests to the exposed endpoints. Example endpoints include:

### Chat Controller

#### POST /api/chat
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

#### GET /api/chat/conversations
Returns a summary of all conversations. Each summary includes the conversation ID and the title.
```json
[
  {
    "ConversationId": "conversation-id",
    "Title": "Conversation title"
  }
]
```

#### GET /api/chat/conversations/{conversationId}
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

#### DELETE /api/chat/conversations/{conversationId}
Deletes a specific conversation by its ID. If the conversation is found and deleted, a success message is returned. Otherwise, a not found message is returned.
```json
{
  "message": "Conversation deleted."
}
```

Refer to the project documentation for detailed information on all available controllers and their endpoints.

## Contributing
Feel free to submit pull requests or open issues for improvements.

## License
This project is licensed under the MIT License.
