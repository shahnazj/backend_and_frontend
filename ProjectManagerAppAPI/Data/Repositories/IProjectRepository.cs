using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllProjectsAsync();
    Task<Project?> GetProjectByIdAsync(int id);
    Task<Project> CreateProjectAsync(Project project);
    Task<Project?> UpdateProjectAsync(int id, Project project);
    Task DeleteProjectAsync(int id);
    Task<List<Project>> GetProjectsByCustomerIdAsync(int customerId);
    Task<List<Project>> GetProjectsByEmployeeIdAsync(int employeeId);
}