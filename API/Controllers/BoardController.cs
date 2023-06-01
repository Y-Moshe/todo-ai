using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/boards")]
public class BoardController : BaseApiController
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    public async Task<ActionResult<Board>> CreateBoard([FromQuery] string todo)
    {
        if (string.IsNullOrEmpty(todo))
            return BadRequest(new ApiErrorResponse(400, "Todo is required"));

        var result = await _boardService.CreateBoard(todo);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> GetBoard(int id)
    {
        var result = await _boardService.GetBoardAsync(id);
        return Ok(result);
    }
}
