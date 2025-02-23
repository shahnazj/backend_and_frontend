using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class CreateServiceDTO
    {
        [Required, MaxLength(150)]
        public required string ServiceName { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public int UnitId { get; set; }
        [Required]
        public int CurrencyId { get; set; }
    }
}
