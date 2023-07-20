using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/todos")]
[Authorize]
public class TodoController : BaseApiController
{
    private readonly ITodoService _todoService;
    private readonly IMapper _mapper;

    public TodoController(ITodoService todoService, IMapper mapper)
    {
        _mapper = mapper;
        _todoService = todoService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetTodo(int id)
    {
        var todo = await _todoService.GetTodoAsync(id, User.GetUserId());
        return Ok(todo);
    }

    [HttpPut("orders")]
    public async Task<ActionResult> SaveTodosOrder(Todo[] orderedTodos)
    {
        await _todoService.SaveTodosOrderAsync(orderedTodos);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateTodo(UpdateTodoDto todoDto)
    {
        var todo = _mapper.Map<Todo>(todoDto);
        todo.AppUserId = User.GetUserId();
        await _todoService.CreateTodoAsync(todo);
        return Ok(todo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Todo>> UpdateTodo(int id, UpdateTodoDto taskDto)
    {
        var todo = _mapper.Map<Todo>(taskDto);
        todo.Id = id;
        todo.AppUserId = User.GetUserId();

        var result = await _todoService.UpdateTodoAsync(todo);

        if (result == null)
            return NotFound(new ApiErrorResponse(404, "Todo not found!"));
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult> UpdateBoardStatus(int id, UpdateStatusDto statusDto)
    {
        string userId = User.GetUserId();
        await _todoService.UpdateTodoStatusAsync(id, userId, statusDto.Status);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task DeleteTodo(int id)
    {
        var todo = new Todo { Id = id, AppUserId = User.GetUserId() };
        await _todoService.DeleteTodoAsync(todo);
    }
}
