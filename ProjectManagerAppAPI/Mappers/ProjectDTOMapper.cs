using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class ProjectDTOMapper
    {
        public static ProjectDTO ToProjectDTO(Project project)
        {
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                StatusId = project.StatusId,
                CustomerId = project.CustomerId,
                EmployeeId = project.EmployeeId,
                ServiceId = project.ServiceId,
            };
        }
        public static Project ToProject(ProjectDTO projectDTO)
        {
            return new Project
            {
                Id = projectDTO.Id,
                Name = projectDTO.Name,
                Description = projectDTO.Description,
                StartDate = projectDTO.StartDate,
                EndDate = projectDTO.EndDate,
                StatusId = projectDTO.StatusId,
                CustomerId = projectDTO.CustomerId,
                EmployeeId = projectDTO.EmployeeId,
                ServiceId = projectDTO.ServiceId,
            };
        }

        public static Project ToProject(CreateProjectDTO createProjectDTO)
        {
            return new Project
            {
                Name = createProjectDTO.Name,
                Description = createProjectDTO.Description,
                StartDate = createProjectDTO.StartDate,
                EndDate = createProjectDTO.EndDate,
                StatusId = createProjectDTO.StatusId,
                CustomerId = createProjectDTO.CustomerId,
                EmployeeId = createProjectDTO.EmployeeId,
                ServiceId = createProjectDTO.ServiceId,
            };
        }
    }
}
