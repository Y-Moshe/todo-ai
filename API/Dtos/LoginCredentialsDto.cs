using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class LoginCredentialsDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$",
      ErrorMessage = "Invalid password, require at least one uppercase, lowercase, digits and minlength of 6!")]
    public string Password { get; set; }
}
