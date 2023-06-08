namespace Core.Excel;

public class ExcelTodo
{
    public string Title { get; set; }
    public IReadOnlyList<ExcelSubTask> SubTasks { get; set; }
}
