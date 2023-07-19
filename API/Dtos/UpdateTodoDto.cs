using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UpdateTodoDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public int Order { get; set; }

    [Required]
    public int BoardId { get; set; }
}
