using Core.Entities;

namespace Core.Interfaces;

public interface IChatGPTService
{
    public Task<Board> GenerateTodoBoardAsync(string todo);
}
