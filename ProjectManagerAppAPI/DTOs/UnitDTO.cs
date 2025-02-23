using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class UnitDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(10)]
        public required string Name { get; set; }
    }
}
