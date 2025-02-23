using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.Services;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectDTO>>> GetAllProjects()
    {
        var projectDtos = await _projectService.GetAllProjectsAsync();
        return Ok(projectDtos);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<List<ProjectDTO>>> GetProjectsByCustomerId(int customerId)
    {
        if (customerId <= 0) return BadRequest("Invalid Customer ID.");

        var projectDtos = await _projectService.GetProjectsByCustomerIdAsync(customerId);
        if (!projectDtos.Any()) return NotFound($"No projects found for customer ID {customerId}.");

        return Ok(projectDtos);
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<ActionResult<List<ProjectDTO>>> GetProjectsByEmployeeId(int employeeId)
    {
        if (employeeId <= 0) return BadRequest("Invalid Employee ID.");

        var projectDtos = await _projectService.GetProjectsByEmployeeIdAsync(employeeId);
        if (!projectDtos.Any()) return NotFound($"No projects found for employee ID {employeeId}.");

        return Ok(projectDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDTO>> GetProjectById(int id)
    {

        var projectDto = await _projectService.GetProjectByIdAsync(id);
        if (projectDto == null) return NotFound($"Project with ID {id} not found.");

        return Ok(projectDto);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDTO>> CreateProject([FromBody] CreateProjectDTO createProjectDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdProject = await _projectService.CreateProjectAsync(createProjectDTO);
        return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectDTO>> UpdateProject(int id, [FromBody] ProjectDTO projectDto)
    {
        if (id != projectDto.Id)
        {
            return BadRequest("ID in URL does not match ID in request body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedProject = await _projectService.UpdateProjectAsync(id, projectDto);
        return Ok(updatedProject);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {

        try
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting project.");
        }
    }
}