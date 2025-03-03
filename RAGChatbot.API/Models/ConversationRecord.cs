using Microsoft.SemanticKernel.ChatCompletion;

namespace RAGChatbot.API.Models
{
    public class Message
    {
        public string Text { get; set; }
        public string Sender { get; set; }
    }

    public class ConversationRecord
    {
        public ChatHistory ChatHistory { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public string Title { get; set; }

        public ConversationRecord(ChatHistory chatHistory)
        {
            ChatHistory = chatHistory;
        }
    }
}
