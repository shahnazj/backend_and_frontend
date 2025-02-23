using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.Models;

public class ContactPerson
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public required string FirstName { get; set; }

    [Required, MaxLength(50)]
    public required string LastName { get; set; }

    [Required, MaxLength(150)]
    public required string Email { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    
}