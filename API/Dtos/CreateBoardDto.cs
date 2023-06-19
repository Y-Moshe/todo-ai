using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class CreateBoardDto
{
    [Required]
    public string Prompt { get; set; }
}
