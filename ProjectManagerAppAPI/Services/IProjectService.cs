using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;

public interface IProjectService
{
    Task<List<ProjectDTO>> GetAllProjectsAsync();
    Task<List<ProjectDTO>> GetProjectsByCustomerIdAsync(int customerId);
    Task<List<ProjectDTO>> GetProjectsByEmployeeIdAsync(int employeeId);
    Task<ProjectDTO?> GetProjectByIdAsync(int id);
    Task<ProjectDTO> CreateProjectAsync(CreateProjectDTO createProjectDTO);
    Task<ProjectDTO?> UpdateProjectAsync(int id, ProjectDTO project);
    Task DeleteProjectAsync(int id);
}