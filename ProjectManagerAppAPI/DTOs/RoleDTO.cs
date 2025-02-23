using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class RoleDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string RoleName { get; set; }
    }
}
