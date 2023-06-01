using Core.Entities;

namespace Core.Interfaces;

public interface ITodoService
{
    Task<Board> CreateBoard(string todo);
    Task<Board> GetBoardAsync(int boardId);

    Task<SubTask> CreateSubTaskAsync(SubTask subTask);
    Task<SubTask> UpdateSubTaskAsync(SubTask subTask);
    Task<SubTask> DeleteSubTaskAsync(int subTaskId);

    Task<IReadOnlyList<SubTask>> ListCompletedSubTasksAsync();
}
