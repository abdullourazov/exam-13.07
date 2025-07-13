using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Car
{
    [Key]
    public int Id { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public int BranchId { get; set; }


    //Навигация:
    public ICollection<Rental> Rentals { get; set; }
    public Branch Branches { get; set; }

}
