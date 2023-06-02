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

    public async Task<IReadOnlyList<SubTask>> ListCompletedSubTasksAsync()
    {
        var spec = new CompletedSubTasksSpec();
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
        if (!await IsExists(subTask.Id)) return null;

        _subTaskRepo.Update(subTask);
        await _subTaskRepo.SaveChangesAsync();
        return subTask;
    }

    public async Task DeleteSubTaskAsync(int subTaskId)
    {
        if (!await IsExists(subTaskId)) return;

        var subTask = new SubTask { Id = subTaskId };
        _subTaskRepo.Delete(subTask);
        await _subTaskRepo.SaveChangesAsync();
    }

    private async Task<bool> IsExists(int subTaskId)
    {
        var subTask = await _subTaskRepo.GetEntityByIdAsync(subTaskId);
        return subTask != null;
    }
}
