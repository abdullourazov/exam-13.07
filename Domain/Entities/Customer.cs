using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    
    
    //Навигация:
    public ICollection<Rental> Rentals { get; set; }
}
