using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class ProjectDTO
    {
        public required int Id { get; set; }

        [Required, MaxLength(200)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int StatusId { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public int ServiceId { get; set; }
    }
}
