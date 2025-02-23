using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.Services;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService ?? throw new ArgumentNullException(nameof(serviceService));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetAllServices()
    {
        var services = await _serviceService.GetAllServicesAsync();
        return Ok(services);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDTO>> GetServiceById(int id)
    {
        try
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            return Ok(service);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpPost]
    public async Task<ActionResult<ServiceDTO>> CreateService([FromBody] CreateServiceDTO createServiceDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdService = await _serviceService.CreateServiceAsync(createServiceDTO);
            return CreatedAtAction(nameof(GetServiceById), new { id = createdService.Id }, createdService);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceDTO>> UpdateService(int id, [FromBody] ServiceDTO serviceDto)
    {
        if (id != serviceDto.Id)
        {
            return BadRequest("ID in URL does not match ID in request body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedService = await _serviceService.UpdateServiceAsync(id, serviceDto);
            if (updatedService == null)
            {
                return NotFound($"Service with ID {id} not found.");
            }

            return Ok(updatedService);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteService(int id)
    {
        try
        {
            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
