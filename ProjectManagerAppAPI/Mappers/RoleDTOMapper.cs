using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class RoleDTOMapper
    {
        public static RoleDTO ToRoleDTO(Role role)
        {
            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
            };
        }

        public static Role ToRole(RoleDTO roleDTO)
        {
            return new Role
            {
                Id = roleDTO.Id,
                RoleName = roleDTO.RoleName,
            };
        }

        public static Role ToRole(CreateRoleDTO createRoleDTO)
        {
            return new Role
            {
                RoleName = createRoleDTO.RoleName,
            };
        }

    }
}