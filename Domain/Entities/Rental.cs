using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Rental
{
    [Key]
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Decimal TotalCost { get; set; }


    //Навигация:
    public Car Car { get; set; }
    public Customer Customer { get; set; }
    public Branch Branch { get; set; }
}
