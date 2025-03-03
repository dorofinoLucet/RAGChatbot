namespace RAGChatBot.WebApp.Services
{
    public class ChatStateService
    {
        private readonly HttpClient _httpClient;

        public ChatStateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<ConversationSummary> ConversationSummaries { get; set; } = new();
        public ConversationDetail? SelectedConversation { get; set; }

        public event Action? OnChange;

        public void SetConversationSummaries(List<ConversationSummary> summaries)
        {
            ConversationSummaries = summaries ?? new List<ConversationSummary>();
            NotifyStateChanged();
        }

        public void SetSelectedConversation(ConversationDetail conversation)
        {
            SelectedConversation = conversation;
            NotifyStateChanged();
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public async Task LoadConversationsAsync()
        {
            try
            {
                var summaries = await _httpClient.GetFromJsonAsync<List<ConversationSummary>>("https://localhost:7233/api/Chat/conversations");
                if (summaries != null)
                {
                    SetConversationSummaries(summaries);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading conversations: {ex.Message}");
            }
        }

        public async Task SelectConversation(string conversationId)
        {
            try
            {
                var conversation = await _httpClient.GetFromJsonAsync<ConversationDetail>($"https://localhost:7233/api/Chat/conversations/{conversationId}");
                if (conversation != null)
                {
                    Console.WriteLine($"Fetched conversation: {conversation.ConversationId}, Messages count: {conversation.Messages?.Count ?? 0}");
                    SelectedConversation = conversation;
                    NotifyStateChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error selecting conversation: {ex.Message}");
            }
        }

        public async Task HandleNewConversation()
        {
            var newConversation = new ConversationSummary
            {
                ConversationId = Guid.NewGuid().ToString(),
                Title = "New Conversation"
            };
            ConversationSummaries.Add(newConversation);
            SelectedConversation = new ConversationDetail
            {
                ConversationId = newConversation.ConversationId,
                Title = newConversation.Title,
                Messages = new List<Message>()
            };
            NotifyStateChanged();
        }

        public async Task DeleteConversation(string conversationId)
        {
            ConversationSummaries.RemoveAll(c => c.ConversationId == conversationId);
            if (SelectedConversation?.ConversationId == conversationId)
            {
                SelectedConversation = null;
            }
            NotifyStateChanged();
        }
    }

    public class ConversationSummary
    {
        public string ConversationId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
    }

    public class ConversationDetail
    {
        public string? ConversationId { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = new();
    }

    public class Message
    {
        public string Text { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty; // "You" or "Bot"
    }

    public class ChatResponse
    {
        public string ConversationId { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
    }
}
