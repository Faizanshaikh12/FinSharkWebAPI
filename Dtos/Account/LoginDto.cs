using System.ComponentModel.DataAnnotations;

namespace FinSharkWebAPI.Dtos.Account;

public class LoginDto
{
    [Required] public string? Username { get; set; }
    
    [Required] public string? Password { get; set; }
}