using Azure.Search.Documents.Models;
using Azure.Search.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Collections.Concurrent;
using System.Text;
using RAGChatbot.API.Models;

namespace RAGChatbot.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatbotController : ControllerBase
    {
        // In-memory store for conversation records, keyed by conversation ID.
        private static readonly ConcurrentDictionary<string, ConversationRecord> Conversations = new();

        private readonly IChatCompletionService _chatService;
        private readonly SearchClient _searchClient;
        private readonly ChatBotOptions _options;
        private readonly ILogger<ChatbotController> _logger;

        public ChatbotController(
            IChatCompletionService chatService,
            SearchClient searchClient,
            ChatBotOptions options,
            ILogger<ChatbotController> logger)
        {
            _chatService = chatService;
            _searchClient = searchClient;
            _options = options;
            _logger = logger;
        }

        // POST: /api/Chat?conversationId={conversationId}
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChatRequest request, [FromQuery] string conversationId = null)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Prompt))
            {
                return BadRequest("Prompt is required.");
            }

            try
            {
                // Retrieve existing conversation or create a new one.
                ConversationRecord conversationRecord;
                if (string.IsNullOrEmpty(conversationId) || !Conversations.TryGetValue(conversationId, out conversationRecord))
                {
                    conversationId = System.Guid.NewGuid().ToString();
                    conversationRecord = new ConversationRecord(new ChatHistory(_options.SystemMessage));
                    Conversations.TryAdd(conversationId, conversationRecord);
                }

                // Enrich the prompt with Azure Search context for internal AI use.
                string enrichedPrompt = await EnrichMessageWithSearchContext(_searchClient, request.Prompt, _options.TopK);

                // Add the original user prompt as a Message object.
                conversationRecord.Messages.Add(new Message
                {
                    Sender = "You",
                    Text = request.Prompt
                });

                // Add the enriched prompt to ChatHistory for the AI to generate a response.
                conversationRecord.ChatHistory.AddUserMessage(enrichedPrompt);

                // Set the conversation title using the original prompt if not already set.
                if (string.IsNullOrEmpty(conversationRecord.Title))
                {
                    string userInput = request.Prompt;
                    conversationRecord.Title = userInput.Length > 30 ? userInput.Substring(0, 30) + "..." : userInput;
                }

                // Get the chat response from Azure OpenAI.
                var chatMessage = await _chatService.GetChatMessageContentAsync(conversationRecord.ChatHistory);

                // Add the bot's response as a Message object.
                conversationRecord.Messages.Add(new Message
                {
                    Sender = "Bot",
                    Text = chatMessage.Content
                });
                conversationRecord.ChatHistory.AddAssistantMessage(chatMessage.Content);

                // Return the response along with the conversation ID.
                return Ok(new { conversationId, response = chatMessage.Content });
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error processing chat request.");
                return StatusCode(500, "Internal server error.");
            }
        }

        // GET: /api/Chat/conversations
        [HttpGet("conversations")]
        public IActionResult GetConversations()
        {
            var summaries = Conversations.Select(c => new
            {
                ConversationId = c.Key,
                Title = !string.IsNullOrWhiteSpace(c.Value.Title)
                            ? c.Value.Title
                            : (c.Value.Messages.FirstOrDefault()?.Text ?? "Empty Conversation")
            });
            return Ok(summaries);
        }

        // GET: /api/Chat/conversations/{conversationId}
        [HttpGet("conversations/{conversationId}")]
        public IActionResult GetConversation(string conversationId)
        {
            if (string.IsNullOrEmpty(conversationId) || !Conversations.TryGetValue(conversationId, out var conversationRecord))
            {
                return NotFound("Conversation not found.");
            }
            return Ok(new
            {
                ConversationId = conversationId,
                Title = conversationRecord.Title,
                Messages = conversationRecord.Messages
            });
        }

        // DELETE: /api/Chat/conversations/{conversationId}
        [HttpDelete("conversations/{conversationId}")]
        public IActionResult DeleteConversation(string conversationId)
        {
            if (string.IsNullOrEmpty(conversationId))
            {
                return BadRequest("ConversationId is required.");
            }
            if (Conversations.TryRemove(conversationId, out var removed))
            {
                return Ok(new { message = "Conversation deleted." });
            }
            else
            {
                return NotFound("Conversation not found.");
            }
        }

        private static async Task<string> EnrichMessageWithSearchContext(SearchClient searchClient, string userInput, int topK)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Relevant Context from Azure Search:]");

            var options = new SearchOptions
            {
                QueryType = SearchQueryType.Semantic,
                Size = topK
            };

            var response = await searchClient.SearchAsync<SearchDocument>(userInput, options);
            foreach (var result in response.Value.GetResults())
            {
                if (result.Document.TryGetValue("content", out object contentValue))
                {
                    sb.AppendLine(contentValue?.ToString());
                }
            }
            sb.AppendLine("[End of Context]");
            sb.AppendLine(userInput);

            return sb.ToString();
        }
    }
}
