namespace Core.Interfaces;

public interface IChatGPTService
{
    public Task<string> GetTasksListAsJsonAsync(string todo);
}
