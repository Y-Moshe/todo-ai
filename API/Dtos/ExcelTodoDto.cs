namespace API.Dtos;

public class ExcelTodoDto
{
    public string Title { get; set; }
    public IReadOnlyList<ExcelSubTaskDto> SubTasks { get; set; }
}
