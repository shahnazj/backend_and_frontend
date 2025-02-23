using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.Services;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitsController : ControllerBase
{
    private readonly IUnitService _unitService;

    public UnitsController(IUnitService unitService)
    {
        _unitService = unitService ?? throw new ArgumentNullException(nameof(unitService));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<UnitDTO>>> GetAllUnits()
    {
        var units = await _unitService.GetAllUnitsAsync();
        return Ok(units);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<UnitDTO>> GetUnitById(int id)
    {
        try
        {
            var unit = await _unitService.GetUnitByIdAsync(id);
            return Ok(unit);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

  
    [HttpPost]
    public async Task<ActionResult<UnitDTO>> CreateUnit([FromBody] CreateUnitDTO createUnitDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdUnit = await _unitService.CreateUnitAsync(createUnitDTO);
            return createdUnit;
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
    public async Task<IActionResult> DeleteUnit(int id)
    {
        try
        {
            await _unitService.DeleteUnitAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
