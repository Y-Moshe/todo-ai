using Core.Entities;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class SubTaskService : ISubTaskService
{
    private readonly IGenericRepository<SubTask> _subTaskRepo;

    public SubTaskService(IGenericRepository<SubTask> repo)
    {
        _subTaskRepo = repo;
    }

    public async Task<IReadOnlyList<SubTask>> ListCompletedUserSubTasksAsync(string userId)
    {
        var spec = new CompletedUserSubTasksSpec(userId);
        var subTasks = await _subTaskRepo.ListAllWithSpecAsync(spec);
        return subTasks;
    }

    public async Task<SubTask> CreateSubTaskAsync(SubTask subTask)
    {
        _subTaskRepo.Add(subTask);
        await _subTaskRepo.SaveChangesAsync();
        return subTask;
    }

    public async Task<SubTask> UpdateSubTaskAsync(SubTask subTask)
    {
        _subTaskRepo.Update(subTask);
        await _subTaskRepo.SaveChangesAsync();
        return subTask;
    }

    public async Task DeleteSubTaskAsync(SubTask subTask)
    {
        _subTaskRepo.Delete(subTask);
        await _subTaskRepo.SaveChangesAsync();
    }

    public async Task SaveSubtasksOrderAsync(SubTask[] orderedSubtasks)
    {
        _subTaskRepo.UpdateRange(orderedSubtasks);
        await _subTaskRepo.SaveChangesAsync();
    }
}
