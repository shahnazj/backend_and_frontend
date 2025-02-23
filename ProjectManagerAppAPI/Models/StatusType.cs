using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.Models;
public class StatusType
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(20)]
    public required string Status { get; set; }
    
}
