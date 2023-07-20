using System.Drawing;
using System.Text.Json;
using Aspose.Cells;
using Aspose.Cells.Utility;
using Core.Entities;
using Core.Excel;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services;

public class BoardService : IBoardService
{
    private readonly IGenericRepository<Board> _boardRepo;

    public BoardService(IGenericRepository<Board> repo, IChatGPTService chatGPTService)
    {
        _boardRepo = repo;
    }

    public async Task<Board> CreateUserBoardAsync(Board board, string userId)
    {
        board.AppUserId = userId;
        foreach (var todo in board.Todos)
        {
            todo.AppUserId = userId;

            foreach (var subtask in todo.SubTasks)
            {
                subtask.AppUserId = userId;
            }
        }

        _boardRepo.Add(board);
        await _boardRepo.SaveChangesAsync();

        return board;
    }

    public async Task<Board[]> CreateUserBoardsAsync(Board[] boards, string userId)
    {
        foreach (var board in boards)
        {
            board.Id = 0;
            board.AppUserId = userId;
            foreach (var todo in board.Todos)
            {
                todo.Id = 0;
                todo.AppUserId = userId;

                foreach (var subtask in todo.SubTasks)
                {
                    subtask.Id = 0;
                    subtask.AppUserId = userId;
                }
            }
        }

        _boardRepo.AddRange(boards);
        await _boardRepo.SaveChangesAsync();

        return boards;
    }

    public async Task<Board> GetUserBoardAsync(int boardId, string userId)
    {
        var spec = new FullyPopulatedUserBoardSpec(boardId, userId);
        return await _boardRepo.GetEntityWithSpecAsync(spec);
    }

    public MemoryStream GenerateBoardExcelFileAsync(ExcelBoard board)
    {
        Workbook workbook = new Workbook();
        Worksheet worksheet = workbook.Worksheets[0];
        worksheet.Name = board.Name;

        Style todosStyle = new CellsFactory().CreateStyle();
        todosStyle.Font.Color = Color.WhiteSmoke;
        todosStyle.Font.IsBold = true;
        todosStyle.Font.Size = 16;
        todosStyle.SetPatternColor(BackgroundType.Solid, Color.Green, Color.WhiteSmoke);
        worksheet.Cells["A1"].SetStyle(todosStyle);

        Style titleStyle = new CellsFactory().CreateStyle();
        titleStyle.Font.Color = Color.WhiteSmoke;
        titleStyle.Font.IsBold = true;
        titleStyle.Font.Size = 12;
        titleStyle.SetPatternColor(BackgroundType.Solid, Color.RoyalBlue, Color.WhiteSmoke);
        worksheet.Cells["A2"].SetStyle(titleStyle);

        Style subTasksStyle = new CellsFactory().CreateStyle();
        subTasksStyle.Font.Color = Color.WhiteSmoke;
        subTasksStyle.Font.IsBold = true;
        subTasksStyle.Font.Size = 12;
        subTasksStyle.SetPatternColor(BackgroundType.Solid, Color.Orange, Color.WhiteSmoke);
        worksheet.Cells["B2"].SetStyle(subTasksStyle);

        string json = JsonSerializer.Serialize(board);
        JsonLayoutOptions layoutOptions = new JsonLayoutOptions { ArrayAsTable = true };
        JsonUtility.ImportData(json, worksheet.Cells, 0, 0, layoutOptions);

        MemoryStream stream = new MemoryStream();
        workbook.Save(stream, SaveFormat.Xlsx);
        return stream;
    }

    public async Task<IReadOnlyList<Board>> ListUserBoardsAsync(string userId)
    {
        var spec = new FullyPopulatedUserBoardSpec(userId);
        return await _boardRepo.ListAllWithSpecAsync(spec);
    }

    public async Task<Board> UpdateUserBoardAsync(Board board)
    {
        _boardRepo.Update(board);
        await _boardRepo.SaveChangesAsync();
        return board;
    }

    public async Task UpdateUserBoardStatusAsync(int boardId, string userId, bool status)
    {
        var board = await this.GetUserBoardAsync(boardId, userId);

        foreach (var todo in board.Todos)
        {
            foreach (var task in todo.SubTasks)
            {
                task.IsDone = status;
            }
        }

        await _boardRepo.SaveChangesAsync();
    }

    public async Task DeleteUserBoardAsync(Board board)
    {
        _boardRepo.Delete(board);
        await _boardRepo.SaveChangesAsync();
    }

    public async Task SaveBoardsOrderAsync(Board[] orderedBoards)
    {
        _boardRepo.UpdateRange(orderedBoards);
        await _boardRepo.SaveChangesAsync();
    }
}
