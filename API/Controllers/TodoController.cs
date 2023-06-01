using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/todos")]
public class TodoController : BaseApiController
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<Board>> CreateBoard([FromQuery] string todo)
    {
        if (string.IsNullOrEmpty(todo))
            return BadRequest(new ApiErrorResponse(400, "Todo is required"));

        var result = await _todoService.CreateBoard(todo);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> GetBoard(int id)
    {
        var result = await _todoService.GetBoardAsync(id);
        return Ok(result);
    }
}
