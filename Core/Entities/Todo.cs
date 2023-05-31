namespace Core.Entities;

public class Todo : BaseEntity
{
    public string Title { get; set; }
    public SubTask[] SubTasks { get; set; }

    public int BoardId { get; set; }
    public Board Board { get; set; }
}
