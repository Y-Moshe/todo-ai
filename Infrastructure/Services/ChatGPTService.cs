using System.Text;
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

    public async Task<string> GetTasksListAsJsonAsync(string todo)
    {
        StringBuilder sb = new StringBuilder("Give me a list of tasks as JSON Format about ");
        sb.Append(todo);
        sb.Append(" While each task is object containing description of the task");
        sb.Append(" and isDone property with default of false");

        var msg = new ChatMessage(ChatMessageRole.User, sb.ToString());
        var request = new ChatRequest
        {
            Messages = new List<ChatMessage> { msg },
            Model = Model.ChatGPTTurbo,
            // Temperature = 0.1,
            // MaxTokens = 50
        };

        var result = await _api.Chat.CreateChatCompletionAsync(request);
        return result.ToString();
    }
}
