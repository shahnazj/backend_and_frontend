using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class CreateUnitDTO
    {

        [Required, MaxLength(10)]
        public required string Name { get; set; }
    }
}
