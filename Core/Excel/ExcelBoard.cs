using System.Text.Json.Serialization;

namespace Core.Excel;

public class ExcelBoard
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public string Name { get; set; }

    public IReadOnlyList<ExcelTodo> Todos { get; set; }
}
