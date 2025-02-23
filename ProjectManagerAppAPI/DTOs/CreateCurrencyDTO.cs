using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class CreateCurrencyDTO
    {

        [Required, MaxLength(10)]
        public required string Name { get; set; }
    }
}
