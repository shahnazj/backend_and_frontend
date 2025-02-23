using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Services;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusTypeController : ControllerBase
{
    private readonly IStatusTypeService _statusTypeService;

    public StatusTypeController(IStatusTypeService statusTypeService)
    {
        _statusTypeService = statusTypeService ?? throw new ArgumentNullException(nameof(statusTypeService));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StatusTypeDTO>>> GetAllStatusTypes()
    {
        var statuses = await _statusTypeService.GetAllStatusTypesAsync();
        return Ok(statuses);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StatusTypeDTO>> GetStatusTypeById(int id)
    {
        var status = await _statusTypeService.GetStatusTypeByIdAsync(id);
        if (status == null)
        {
            return NotFound($"StatusType with ID {id} not found.");
        }

        return Ok(status);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StatusTypeDTO>> CreateStatusType([FromBody] CreateStatusTypeDTO createStatusTypeDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdStatus = await _statusTypeService.CreateStatusTypeAsync(createStatusTypeDTO);
        return CreatedAtAction(nameof(GetStatusTypeById), new { id = createdStatus.Id }, createdStatus);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StatusTypeDTO>> UpdateStatusType(int id, [FromBody] StatusTypeDTO statusTypeDto)
    {
        if (id != statusTypeDto.Id)
        {
            return BadRequest("ID in URL does not match ID in request body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedStatus = await _statusTypeService.UpdateStatusTypeAsync(id, statusTypeDto);
        if (updatedStatus == null)
        {
            return NotFound($"StatusType with ID {id} not found.");
        }

        return Ok(updatedStatus);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStatusType(int id)
    {
        var status = await _statusTypeService.GetStatusTypeByIdAsync(id);
        if (status == null)
        {
            return NotFound($"StatusType with ID {id} not found.");
        }

        await _statusTypeService.DeleteStatusTypeAsync(id);
        return NoContent();
    }
}
