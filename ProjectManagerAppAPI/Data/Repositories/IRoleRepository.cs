using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleByIdAsync(int id);
    Task<Role?> UpdateRoleAsync(int id, Role role);
    Task<Role> CreateRoleAsync(Role role);
    Task<bool> DeleteRoleAsync(int id);
}
