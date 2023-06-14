using API.Dtos;
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

    [HttpPost("orders")]
    public async Task<ActionResult> SaveTodosOrder(SaveTodosOrderDto order)
    {
        await _todoService.SaveTodosOrderAsync(order.BoardId, order.Todos);
        return Ok();
    }
}
