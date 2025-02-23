using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class ContactPersonDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required, MaxLength(150)]
        public required string Email { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
