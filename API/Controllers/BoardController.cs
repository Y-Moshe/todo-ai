using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Excel;
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

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Board>>> GetBoards()
    {
        var result = await _boardService.GetBoardsAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Board>> CreateBoard(CreateBoardDto payload)
    {
        if (string.IsNullOrEmpty(payload.Prompt))
            return BadRequest(new ApiErrorResponse(400, "Prompt is required"));

        var result = await _boardService.CreateBoardAsync(payload.Prompt);
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
    public async Task<IActionResult> GetBoardExcel(int id)
    {
        var board = await _boardService.GetBoardAsync(id);
        if (board == null) return NotFound(
            new ApiErrorResponse(404, "Board was not found not"));

        var excelMappedBoard = _mapper.Map<ExcelBoard>(board);
        MemoryStream stream = _boardService
            .GenerateBoardExcelFileAsync(excelMappedBoard);

        string fileName = $"{board.Name}.xlsx";
        string mimeType = "application/octet-stream";
        return File(stream.ToArray(), mimeType, fileName);
    }
}
