using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UpdateBoardDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int Order { get; set; }
}
