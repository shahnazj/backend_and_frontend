using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;
public interface IRoleService
{
    Task<List<RoleDTO>> GetAllRolesAsync();
    Task<RoleDTO?> GetRoleByIdAsync(int id);
    Task<RoleDTO> CreateRoleAsync(CreateRoleDTO createRoleDTO);
    Task<bool> DeleteRoleAsync(int id);
}
