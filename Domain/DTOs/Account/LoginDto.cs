using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Account;

public class LoginDto
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Password { get; set; }
}


