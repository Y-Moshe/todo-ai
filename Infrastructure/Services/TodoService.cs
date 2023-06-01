using System.Text.Json;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class TodoService : ITodoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IChatGPTService _chatGPTService;

    public TodoService(IUnitOfWork unitOfWork, IChatGPTService chatGPTService)
    {
        _chatGPTService = chatGPTService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Board> CreateBoard(string todo)
    {
        var board = await _chatGPTService.GenerateTodoBoardAsync(todo);

        _unitOfWork.Repository<Board>().Add(board);
        await _unitOfWork.SaveChangesAsync();

        return board;
    }

    public async Task<Board> GetBoardAsync(int boardId)
    {
        var spec = new FullyPopulatedBoardSpec(boardId);
        return await _unitOfWork.Repository<Board>().GetEntityWithSpecAsync(spec);
    }

    public Task<SubTask> CreateSubTaskAsync(SubTask subTask)
    {
        throw new NotImplementedException();
    }

    public Task<SubTask> UpdateSubTaskAsync(SubTask subTask)
    {
        throw new NotImplementedException();
    }

    public Task<SubTask> DeleteSubTaskAsync(int subTaskId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<SubTask>> ListCompletedSubTasksAsync()
    {
        throw new NotImplementedException();
    }
}
