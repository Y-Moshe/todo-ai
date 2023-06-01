using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class BoardService : IBoardService
{
    private readonly IGenericRepository<Board> _boardRepo;
    private readonly IChatGPTService _chatGPTService;

    public BoardService(IGenericRepository<Board> repo, IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
        _boardRepo = repo;
    }

    public async Task<Board> CreateBoard(string todo)
    {
        var board = await _chatGPTService.GenerateTodoBoardAsync(todo);

        _boardRepo.Add(board);
        await _boardRepo.SaveChangesAsync();

        return board;
    }

    public async Task<Board> GetBoardAsync(int boardId)
    {
        var spec = new FullyPopulatedBoardSpec(boardId);
        return await _boardRepo.GetEntityWithSpecAsync(spec);
    }
}
