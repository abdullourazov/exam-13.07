using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Account;

public class RegisterCustomerDto
{

    [Required, MaxLength(150)]
    public string FullName { get; set; }
    [Required]
    public string Password { get; set; }
}
