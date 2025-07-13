using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Branch
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }    
    
    
    //Навигация:
    public ICollection<Car> Cars { get; set; }
}
