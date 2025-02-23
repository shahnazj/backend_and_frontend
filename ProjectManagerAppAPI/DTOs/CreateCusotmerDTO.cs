using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class CreateCustomerDTO
    {
        [Required, MaxLength(50)]
        public required string Name { get; set; }

        [Required, MaxLength(150)]
        public required string Email { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
