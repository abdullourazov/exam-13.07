using System.Runtime;
using Domain.Paginations;

namespace Domain.Filters;

public class CarFilter : ValidFilter
{
    public string? Model { get; set; }
    public string? Manufacturer { get; set; }
    public int? Year { get; set; }
}
