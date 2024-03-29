using Core.Entities;

namespace Core.Interfaces;

public interface ISubTaskService
{
    Task<IReadOnlyList<SubTask>> ListCompletedUserSubTasksAsync(string userId);

    Task SaveSubtasksOrderAsync(SubTask[] orderedSubtasks);
    Task<SubTask> CreateSubTaskAsync(SubTask subTask);
    Task<SubTask> UpdateSubTaskAsync(SubTask subTask);
    Task DeleteSubTaskAsync(SubTask subTask);
}
