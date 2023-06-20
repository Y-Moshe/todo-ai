using System.ComponentModel.DataAnnotations;
using Core.Entities;

namespace API.Dtos;

public class CreateBoardsDto
{
    [Required]
    public Board[] Boards { get; set; }
}
