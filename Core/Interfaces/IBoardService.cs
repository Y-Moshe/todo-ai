using Core.Entities;

namespace Core.Interfaces;

public interface IBoardService
{
    Task<Board> CreateBoard(string todo);
    Task<Board> GetBoardAsync(int boardId);
}
