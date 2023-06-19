using Core.Entities;

namespace Core.Interfaces;

public interface IChatGPTService
{
    public Task<Board> GenerateBoardAsync(string todo);
}
