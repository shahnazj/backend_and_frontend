using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class ServiceDTO
    {
        public int Id { get; set; }

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
