namespace Domain.DTOs.Rental;

public class CreateRentalDto
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int BranchId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Decimal TotalCost { get; set; }
}
