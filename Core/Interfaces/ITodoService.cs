using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces;

public interface ITodoService
{
    Task<IReadOnlyList<Todo>> GetBoardTodosAsync(int boardId);
    Task SaveTodosOrderAsync(int boardId, List<TodoOrderSaveDto> todos);
}
