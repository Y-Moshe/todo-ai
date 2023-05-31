namespace Core.Entities;

public class Board : BaseEntity
{
    public string Name { get; set; }
    public Todo[] Todos { get; set; }
}
