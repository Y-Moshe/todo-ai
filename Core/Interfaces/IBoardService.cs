using Core.Entities;
using Core.Excel;

namespace Core.Interfaces;

public interface IBoardService
{
    Task<IReadOnlyList<Board>> ListUserBoardsAsync(string userId);
    Task<Board> GetUserBoardAsync(int boardId, string userId);

    Task<Board> CreateUserBoardAsync(Board board, string userId);
    Task<Board[]> CreateUserBoardsAsync(Board[] boards, string userId);

    Task SaveBoardsOrderAsync(Board[] orderedBoards);
    Task<Board> UpdateUserBoardAsync(Board board);
    Task UpdateUserBoardStatusAsync(int boardId, string userId, bool status);
    Task DeleteUserBoardAsync(Board board);

    MemoryStream GenerateBoardExcelFileAsync(ExcelBoard board);
}
