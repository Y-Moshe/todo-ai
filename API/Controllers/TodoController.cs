using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using API.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/todos")]
public class TodoController : BaseApiController
{
    private readonly IChatGPTService _chatGPTService;

    public TodoController(IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetTodos([FromQuery] string todo)
    {
        if (string.IsNullOrEmpty(todo))
            return BadRequest(new ApiErrorResponse(400, "Todo is required"));

        var result = await _chatGPTService.GetTasksListAsJsonAsync(todo);
        return result;
    }
}
