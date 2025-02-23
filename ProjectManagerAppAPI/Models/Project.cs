using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagerAppAPI.Models;
public class Project
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public required string Name { get; set; }

    public required string Description { get; set; }

    [Required]
    public required DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [ForeignKey("StatusType")]
    public int StatusId { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }

    [ForeignKey("Service")]
    public int ServiceId { get; set; }
}