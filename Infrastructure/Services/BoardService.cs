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
        JsonLayoutOptions layoutOptions =
            new JsonLayoutOptions { ArrayAsTable = true };
        JsonUtility.ImportData(json, worksheet.Cells, 0, 0, layoutOptions);

        MemoryStream stream = new MemoryStream();
        workbook.Save(stream, SaveFormat.Xlsx);
        return stream;
    }

    public async Task<IReadOnlyList<Board>> GetUserBoardsAsync(string userId)
    {
        var spec = new FullyPopulatedUserBoardSpec(userId);
        return await _boardRepo.ListAllWithSpecAsync(spec);
    }
}
