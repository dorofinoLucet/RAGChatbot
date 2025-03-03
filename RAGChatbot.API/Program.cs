using Azure;
using Azure.Search.Documents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using RAGChatbot.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add configuration from appsettings.json
var configuration = builder.Configuration;

// Add CORS policy to allow your Blazor client. 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:7043")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
    });
});

// Add controllers and Swagger support.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Extract Azure OpenAI settings.
var openAiConfig = configuration.GetSection("AzureOpenAI");
string openAiEndpoint = openAiConfig["Endpoint"];
string openAiApiKey = openAiConfig["ApiKey"];
string modelId = openAiConfig["Model"];
string systemMessage = openAiConfig["SystemMessage"] ?? "You are an AI assistant that helps people find information.";

// Extract Azure Search settings.
var azureSearchConfig = configuration.GetSection("AzureSearch");
string searchServiceName = azureSearchConfig["ServiceName"];
string searchApiKey = azureSearchConfig["ApiKey"];
string indexName = azureSearchConfig["IndexName"];
int topK = int.Parse(azureSearchConfig["TopK"]);
string searchEndpoint = $"https://{searchServiceName}.search.windows.net/";

// Configure the Semantic Kernel.
var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddLogging(); // re-use logging configuration if needed.
kernelBuilder.AddAzureOpenAIChatCompletion(modelId, openAiEndpoint, openAiApiKey);
var kernel = kernelBuilder.Build();

// Retrieve the chat completion service and register it.
IChatCompletionService chatService = kernel.Services.GetService<IChatCompletionService>();
builder.Services.AddSingleton(chatService);

// Create and register the Azure Search client.
var searchClient = new SearchClient(new Uri(searchEndpoint), indexName, new AzureKeyCredential(searchApiKey));
builder.Services.AddSingleton(searchClient);

// Register ChatBot options.
builder.Services.AddSingleton(new ChatBotOptions { SystemMessage = systemMessage, TopK = topK });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatBot API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

// Enable CORS before other middlewares
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
