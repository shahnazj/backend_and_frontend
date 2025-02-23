using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.Services;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDTO>> GetRoleById(int id)
    {
        try
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<RoleDTO>> CreateRole([FromBody] CreateRoleDTO createRoleDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdRole = await _roleService.CreateRoleAsync(createRoleDTO);
            return CreatedAtAction(nameof(GetRoleById), new { id = createdRole.Id }, createdRole);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        try
        {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
