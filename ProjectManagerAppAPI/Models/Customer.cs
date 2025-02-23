using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public required string Name { get; set; }
    
    [Required, MaxLength(150)]
    public required string Email { get; set; }
    
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }
    
    public ICollection<ContactPerson> ContactPersons { get; set; } = new HashSet<ContactPerson>();
    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
}