using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities;

public class Todo : BaseEntity
{
    public string Title { get; set; }
    public IReadOnlyList<SubTask> SubTasks { get; set; }
    public int Order { get; set; }

    [ConcurrencyCheck]
    public string AppUserId { get; set; }
    public int BoardId { get; set; }
    [JsonIgnore]
    public Board Board { get; set; }
}
