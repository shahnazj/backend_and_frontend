using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.Models;

public class Currency
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(10)]
    public required string Name { get; set; }
    
    //public ICollection<Service> Services { get; set; } = new HashSet<Service>();
}