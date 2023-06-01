using System.Text;
using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace Infrastructure.Services;

public class ChatGPTService : IChatGPTService
{
    private readonly IConfiguration _config;
    private readonly OpenAIAPI _api;

    public ChatGPTService(IConfiguration config)
    {
        _config = config;
        _api = new OpenAIAPI(_config["OpenAPIKey"]);
    }

    public async Task<Board> GenerateTodoBoardAsync(string todo)
    {
        StringBuilder sb = new StringBuilder(
            $"Give me a boards of tasks as JSON Format about {todo}");
        sb.Append(" with the following structure: ");
        sb.Append(@"
            class Board
            {
                string name { get; set; }
                Todo[] todos { get; set; }
            }

            class Todo
            {
                string title { get; set; }
                SubTask[] subTasks { get; set; }
            }

            class SubTask
            {
                public string text { get; set; }
                public bool isDone { get; set; }
            }
        ");
        sb.Append(" the isDone property with default of false");

        var msg = new ChatMessage(ChatMessageRole.User, sb.ToString());
        var request = new ChatRequest
        {
            Messages = new List<ChatMessage> { msg },
            Model = Model.ChatGPTTurbo,
            // Temperature = 0.1,
            // MaxTokens = 50
        };

        var result = await _api.Chat.CreateChatCompletionAsync(request);
        var jsonOptions = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };

        return JsonSerializer.Deserialize<Board>(
            result.ToString(), jsonOptions);
    }
}
