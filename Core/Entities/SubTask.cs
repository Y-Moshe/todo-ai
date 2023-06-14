using System.Text.Json.Serialization;

namespace Core.Entities;

public class SubTask : BaseEntity
{
    public string Text { get; set; }
    public bool IsDone { get; set; }
    public int Order { get; set; }

    public int TodoId { get; set; }
    [JsonIgnore]
    public Todo Todo { get; set; }
}
