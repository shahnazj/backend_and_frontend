using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerAppAPI.Models;

public class Employee
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public required string FirstName { get; set; }
    
    [Required, MaxLength(50)]
    public required string LastName { get; set; }
    
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    
    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
}