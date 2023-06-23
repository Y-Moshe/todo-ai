using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Dtos;

public class SubTaskDto
{
    // To hide in Swagger documentation for update and create operations
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    public string Text { get; set; }
    [Required]
    public bool IsDone { get; set; }
    [Required]
    public int TodoId { get; set; }

    public string AppUserId { get; set; }
}
