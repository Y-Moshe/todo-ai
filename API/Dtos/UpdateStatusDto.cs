using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class UpdateStatusDto
{
    [Required]
    public bool Status { get; set; }
}
