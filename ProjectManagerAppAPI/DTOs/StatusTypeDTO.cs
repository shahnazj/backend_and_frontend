using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class StatusTypeDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public required string Status { get; set; }
    }
}
