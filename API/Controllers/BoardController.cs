using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Excel;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/boards")]
[Authorize]
public class BoardController : BaseApiController
{
    private readonly IBoardService _boardService;
    private readonly IMapper _mapper;
    private readonly IChatGPTService _chatGPTService;

    public BoardController(IBoardService boardService, IMapper mapper, IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
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
    [AllowAnonymous]
    public async Task<ActionResult<Board>> CreateBoard(CreateBoardDto payload)
    {
        bool isLoggedIn = User.Identity.IsAuthenticated;

        var board = await _chatGPTService.GenerateBoardAsync(payload.Prompt);
        Board result = board;

        if (isLoggedIn)
            result = await _boardService.CreateBoardAsync(board);

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
