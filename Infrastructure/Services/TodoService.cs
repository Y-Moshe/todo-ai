using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class TodoService : ITodoService
{
    private readonly IGenericRepository<Todo> _repository;

    public TodoService(IGenericRepository<Todo> repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<Todo>> GetBoardTodosAsync(int boardId)
    {
        var spec = new PopulatedBoardTodosSpec(boardId);
        return await _repository.ListAllWithSpecAsync(spec);
    }

    public async Task SaveTodosOrderAsync(int boardId, List<TodoOrderSaveDto> orderedTodos)
    {
        var todos = await this.GetBoardTodosAsync(boardId);
        foreach (var todo in todos)
        {
            var shortTodo = orderedTodos.Find(a => a.Id == todo.Id);
            todo.Order = shortTodo.Order;
        }

        await _repository.SaveChangesAsync();
    }
}
