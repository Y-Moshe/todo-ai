using System.Text.Json.Serialization;

namespace API.Dtos;

public class ExcelBoardDto
{
    [JsonIgnore]
    public int Id { get; set; }
    [JsonIgnore]
    public string Name { get; set; }

    public IReadOnlyList<ExcelTodoDto> Todos { get; set; }
}
