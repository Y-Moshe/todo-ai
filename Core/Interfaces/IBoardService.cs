using Core.Entities;
using Core.Excel;

namespace Core.Interfaces;

public interface IBoardService
{
    Task<Board> CreateBoard(string todo);
    Task<Board> GetBoardAsync(int boardId);
    MemoryStream GenerateBoardExcelFileAsync(ExcelBoard board);
}
