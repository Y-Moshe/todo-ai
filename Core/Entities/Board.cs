namespace Core.Entities;

public class Board : BaseEntity
{
    public string Name { get; set; }
    public IReadOnlyList<Todo> Todos { get; set; }
    public int Order { get; set; }

    public string AppUserId { get; set; }
}
