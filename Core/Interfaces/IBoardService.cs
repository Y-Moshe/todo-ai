using Core.Entities;
using Core.Excel;

namespace Core.Interfaces;

public interface IBoardService
{
    Task<IReadOnlyList<Board>> GetUserBoardsAsync(string userId);
    Task<Board> GetUserBoardAsync(int boardId, string userId);
    Task<Board> CreateUserBoardAsync(Board board, string userId);
    MemoryStream GenerateBoardExcelFileAsync(ExcelBoard board);
}
