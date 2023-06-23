using Core.Dtos;

namespace API.Dtos;

public class SaveItemsOrderDto
{
    public int? BoardId { get; set; }
    public int? TodoId { get; set; }
    public List<OrderedItem> Todos { get; set; }
}
