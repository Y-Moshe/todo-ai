using API.Dtos;
using API.Errors;
using API.Extensions;
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
    private readonly IChatGPTService _chatGPTService;
    private readonly IMapper _mapper;

    public BoardController(IBoardService boardService, IMapper mapper, IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
        _boardService = boardService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Board>>> GetUserBoards()
    {
        string userId = User.GetUserId();
        var result = await _boardService.ListUserBoardsAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Board>> CreateBoard(CreateBoardDto payload)
    {
        bool isLoggedIn = User.Identity.IsAuthenticated;
        string userId = User.GetUserId();

        var board = await _chatGPTService.GenerateBoardAsync(payload.Prompt);
        Board result = board;

        if (isLoggedIn)
            result = await _boardService.CreateUserBoardAsync(board, userId);

        return Ok(result);
    }

    [HttpPost("many")]
    public async Task<ActionResult<Board>> AddManyBoards(CreateBoardsDto payload)
    {
        string userId = User.GetUserId();
        var result = await _boardService.CreateUserBoardsAsync(
            payload.Boards, userId);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> GetBoard(int id)
    {
        string userId = User.GetUserId();
        var result = await _boardService.GetUserBoardAsync(id, userId);

        if (result == null) return NotFound(
            new ApiErrorResponse(404, "Board was not found not"));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Board>> UpdateBoard(
        int id, UpdateBoardDto boardDto)
    {
        string userId = User.GetUserId();
        var board = _mapper.Map<Board>(boardDto);
        board.Id = id;
        board.AppUserId = userId;

        var result = await _boardService.UpdateUserBoardAsync(board);

        if (result == null) return NotFound(
            new ApiErrorResponse(404, "Board was not found not"));
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult> UpdateBoardStatus(
        int id, UpdateStatusDto statusDto)
    {
        string userId = User.GetUserId();
        await _boardService.UpdateUserBoardStatusAsync(
            id, userId, statusDto.Status);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBoard(int id)
    {
        string userId = User.GetUserId();
        var board = new Board { Id = id, AppUserId = userId };
        await _boardService.DeleteUserBoardAsync(board);
        return Ok();
    }

    [HttpGet("{id}/excel")]
    public async Task<IActionResult> GetBoardExcel(int id)
    {
        string userId = User.GetUserId();
        var board = await _boardService.GetUserBoardAsync(id, userId);
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
