using Core.Dtos;

namespace API.Dtos;

public class SaveTodosOrderDto
{
    public int BoardId { get; set; }
    public List<TodoOrderSaveDto> Todos { get; set; }
}
