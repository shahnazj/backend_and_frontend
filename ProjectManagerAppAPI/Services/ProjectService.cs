using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.Mappers;

namespace ProjectManagerAppAPI.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository, IEmployeeRepository employeeRepository)
    {
        _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    public async Task<List<ProjectDTO>> GetAllProjectsAsync()
    {
        var projects = await _projectRepository.GetAllProjectsAsync();
        return projects.Select(ProjectDTOMapper.ToProjectDTO).ToList();
    }

    public async Task<List<ProjectDTO>> GetProjectsByCustomerIdAsync(int customerId)
    {
        if (customerId <= 0)
        {
            throw new ArgumentException("Customer ID must be greater than zero.");
        }

        var projects = await _projectRepository.GetProjectsByCustomerIdAsync(customerId);
        return projects.Select(ProjectDTOMapper.ToProjectDTO).ToList();
    }

    public async Task<ProjectDTO?> GetProjectByIdAsync(int id)
    {

        var project = await _projectRepository.GetProjectByIdAsync(id);
        if (project == null)
        {
            throw new KeyNotFoundException($"Project with ID {id} not found.");
        }

        return ProjectDTOMapper.ToProjectDTO(project);
    }

    public async Task<ProjectDTO> CreateProjectAsync(CreateProjectDTO createProjectDTO)
    {
        if (string.IsNullOrWhiteSpace(createProjectDTO.Name))
        {
            throw new ArgumentException("Project name cannot be empty.");
        }


        var customer = await _customerRepository.GetCustomerByIdAsync(createProjectDTO.CustomerId);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {createProjectDTO.CustomerId} does not exist.");
        }

        var employee = await _employeeRepository.GetEmployeeByIdAsync(createProjectDTO.EmployeeId);
        if (employee == null)
        {
            throw new KeyNotFoundException($"Employee with ID {createProjectDTO.EmployeeId} does not exist.");
        }

        var project = ProjectDTOMapper.ToProject(createProjectDTO);
        var createdProject = await _projectRepository.CreateProjectAsync(project);
        return ProjectDTOMapper.ToProjectDTO(createdProject);
    }

    public async Task<ProjectDTO?> UpdateProjectAsync(int id, ProjectDTO projectDto)
    {

        var existingProject = await _projectRepository.GetProjectByIdAsync(id);
        if (existingProject == null)
        {
            throw new KeyNotFoundException($"Project with ID {id} not found.");
        }

        existingProject.Name = projectDto.Name ?? existingProject.Name;

        if (projectDto.CustomerId > 0 && projectDto.CustomerId != existingProject.CustomerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(projectDto.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {projectDto.CustomerId} does not exist.");
            }
            existingProject.CustomerId = projectDto.CustomerId;
        }

        var updatedProject = await _projectRepository.UpdateProjectAsync(id, ProjectDTOMapper.ToProject(projectDto));
        if (updatedProject == null)
        {
            throw new InvalidOperationException("An error occurred while updating the project.");
        }
        return ProjectDTOMapper.ToProjectDTO(updatedProject);
    }

    public async Task DeleteProjectAsync(int id)
    {

        var existingProject = await _projectRepository.GetProjectByIdAsync(id);
        if (existingProject == null)
        {
            throw new KeyNotFoundException($"Project with ID {id} not found.");
        }

        await _projectRepository.DeleteProjectAsync(id);
    }

    public async Task<List<ProjectDTO>> GetProjectsByEmployeeIdAsync(int employeeId)
    {
        if (employeeId <= 0)
        {
            throw new ArgumentException("Employee ID must be greater than zero.");
        }
        var projects = await _projectRepository.GetProjectsByEmployeeIdAsync(employeeId);
        return projects.Select(ProjectDTOMapper.ToProjectDTO).ToList();
    }
}

