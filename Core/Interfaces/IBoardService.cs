using Core.Entities;
using Core.Excel;

namespace Core.Interfaces;

public interface IBoardService
{
    Task<IReadOnlyList<Board>> GetBoardsAsync();
    Task<Board> CreateBoardAsync(Board board);
    Task<Board> GetBoardAsync(int boardId);
    MemoryStream GenerateBoardExcelFileAsync(ExcelBoard board);
}
