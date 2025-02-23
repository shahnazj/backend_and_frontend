using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.Models;

public class Unit
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(10)]
    public required string Name { get; set; }
    
}