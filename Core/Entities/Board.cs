namespace Core.Entities;

public class Board : BaseEntity
{
    public string Name { get; set; }
    public IReadOnlyList<Todo> Todos { get; set; }
}
