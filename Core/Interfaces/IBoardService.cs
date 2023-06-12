using Core.Entities;
using Core.Excel;

namespace Core.Interfaces;

public interface IBoardService
{
    Task<IReadOnlyList<Board>> GetBoardsAsync();
    Task<Board> CreateBoardAsync(string prompt);
    Task<Board> GetBoardAsync(int boardId);
    MemoryStream GenerateBoardExcelFileAsync(ExcelBoard board);
}
