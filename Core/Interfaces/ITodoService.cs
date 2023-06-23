using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces;

public interface ITodoService
{
    Task<IReadOnlyList<Todo>> GetBoardTodosAsync(int boardId);
    Task SaveTodosOrderAsync(int boardId, List<OrderedItem> todos);

    Task<Todo> CreateTodoAsync(Todo todo);
    Task<Todo> UpdateTodoAsync(Todo todo);
    Task UpdateTodoStatusAsync(int todoId, string userId, bool status);
    Task DeleteTodoAsync(Todo todo);
}
