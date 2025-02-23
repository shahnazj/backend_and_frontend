using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class CreateStatusTypeDTO
    {

        [Required, MaxLength(20)]
        public required string Status { get; set; }
    }
}
