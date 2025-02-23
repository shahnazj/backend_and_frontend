using System.ComponentModel.DataAnnotations;

namespace ProjectManagerAppAPI.DTOs
{
    public class CreateRoleDTO
    {

        [Required, MaxLength(50)]
        public required string RoleName { get; set; }
    }
}
