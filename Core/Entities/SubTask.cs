namespace Core.Entities;

public class SubTask : BaseEntity
{
    public string Text { get; set; }
    public bool IsDone { get; set; }

    public int TodoId { get; set; }
    public Todo Todo { get; set; }
}
