using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/boards")]
public class BoardController : BaseApiController
{
    private readonly IBoardService _boardService;
    private readonly IMapper _mapper;

    public BoardController(IBoardService boardService, IMapper mapper)
    {
        _mapper = mapper;
        _boardService = boardService;
    }

    [HttpPost]
    public async Task<ActionResult<Board>> CreateBoard(CreateBoardDto payload)
    {
        if (string.IsNullOrEmpty(payload.Todo))
            return BadRequest(new ApiErrorResponse(400, "Todo is required"));

        var result = await _boardService.CreateBoard(payload.Todo);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> GetBoard(int id)
    {
        var result = await _boardService.GetBoardAsync(id);

        if (result == null) return NotFound(
            new ApiErrorResponse(404, "Board was not found not"));
        return Ok(result);
    }

    [HttpGet("{id}/excel")]
    public async Task<ActionResult<Board>> GetBoardExcel(int id)
    {
        var board = await _boardService.GetBoardAsync(id);
        if (board == null) return NotFound(
            new ApiErrorResponse(404, "Board was not found not"));

        var excelMappedBoard = _mapper.Map<ExcelBoardDto>(board);
        _boardService.GenerateBoardExcelFileAsync(excelMappedBoard);

        return Ok();
    }
}
