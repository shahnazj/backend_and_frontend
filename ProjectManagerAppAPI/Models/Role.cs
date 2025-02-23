using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.Models;
public class Role
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(50)]
    public required string RoleName { get; set; }
    
    //public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
}