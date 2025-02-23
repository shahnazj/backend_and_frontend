using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetAllProjectsAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<List<Project>> GetProjectsByCustomerIdAsync(int customerId)
    {
        return await _context.Projects
            .Where(p => p.CustomerId == customerId)
            .ToListAsync();
    }
    public async Task<List<Project>> GetProjectsByEmployeeIdAsync(int employeeId)
    {
        return await _context.Projects
            .Where(p => p.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<Project?> UpdateProjectAsync(int id, Project project)
    {
        var existingProject = await _context.Projects.FindAsync(id);
        if (existingProject != null)
        {
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.StatusId = project.StatusId;
            existingProject.CustomerId = project.CustomerId;
            existingProject.EmployeeId = project.EmployeeId;
            existingProject.ServiceId = project.ServiceId;

            await _context.SaveChangesAsync();
        }
        return existingProject;
    }

    public async Task DeleteProjectAsync(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}