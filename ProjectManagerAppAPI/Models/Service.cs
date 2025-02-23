using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerAppAPI.Models;
public class Service
{
    [Key]
    public int Id { get; set; }
    
    [Required, MaxLength(150)]
    public required string ServiceName { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [ForeignKey("Unit")]
    public int UnitId { get; set; }
    public Unit? Unit { get; set; }
    
    [ForeignKey("Currency")]
    public int CurrencyId { get; set; }
    public Currency? Currency { get; set; }
    
    public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
}