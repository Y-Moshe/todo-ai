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

    public async Task<Todo> CreateTodoAsync(Todo todo)
    {
        _repository.Add(todo);
        await _repository.SaveChangesAsync();
        return todo;
    }

    public async Task DeleteTodoAsync(Todo todo)
    {
        _repository.Delete(todo);
        await _repository.SaveChangesAsync();
    }

    public async Task SaveTodosOrderAsync(Todo[] orderedTodos)
    {
        _repository.UpdateRange(orderedTodos);
        await _repository.SaveChangesAsync();
    }

    public async Task<Todo> UpdateTodoAsync(Todo todo)
    {
        _repository.Update(todo);
        await _repository.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> GetTodoAsync(int todoId, string userId)
    {
        var spec = new PopulatedTodoSpec(todoId, userId);
        return await _repository.GetEntityWithSpecAsync(spec);
    }

    public async Task UpdateTodoStatusAsync(int todoId, string userId, bool status)
    {
        var spec = new PopulatedTodoSpec(todoId, userId);
        var todo = await _repository.GetEntityWithSpecAsync(spec);

        foreach (var task in todo.SubTasks)
        {
            task.IsDone = status;
        }

        await _repository.SaveChangesAsync();
    }
}
