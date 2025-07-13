namespace Domain.DTOs.Car;

public class CreateCarDto
{
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
}
