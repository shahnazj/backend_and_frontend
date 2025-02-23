using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;

namespace ProjectManagerAppAPI.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleDTO> CreateRoleAsync(CreateRoleDTO createRoleDto)
    {
        if (string.IsNullOrWhiteSpace(createRoleDto.RoleName))
        {
            throw new ArgumentException("RoleName name cannot be empty.");
        }

        var role = RoleDTOMapper.ToRole(createRoleDto);
        var createdRole = await _roleRepository.CreateRoleAsync(role);
        return RoleDTOMapper.ToRoleDTO(createdRole);
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var existingRole = await _roleRepository.GetRoleByIdAsync(id);
        if (existingRole == null)
        {
            throw new KeyNotFoundException($"Role with ID {id} not found.");
        }
        bool result = await _roleRepository.DeleteRoleAsync(id);
        return result;
        
    }

    public async Task<List<RoleDTO>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        return roles.Select(RoleDTOMapper.ToRoleDTO).ToList();
    }

    public async Task<RoleDTO?> GetRoleByIdAsync(int id)
    {
        var role = await _roleRepository.GetRoleByIdAsync(id);
        if (role == null)
        {
            throw new KeyNotFoundException($"Role with ID {id} not found.");
        }
        return RoleDTOMapper.ToRoleDTO(role);
    }
}