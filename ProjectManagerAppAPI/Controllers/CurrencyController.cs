using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.Services;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrenciesController(ICurrencyService currencyService)
    {
        _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CurrencyDTO>>> GetAllCurrencies()
    {
        var currencies = await _currencyService.GetAllCurrenciesAsync();
        return Ok(currencies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CurrencyDTO>> GetCurrencyById(int id)
    {
        try
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            return Ok(currency);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<CurrencyDTO>> CreateCurrency([FromBody] CreateCurrencyDTO createCurrencyDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdCurrency = await _currencyService.CreateCurrencyAsync(createCurrencyDTO);
            return createdCurrency;
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
    public async Task<IActionResult> DeleteCurrency(int id)
    {
        try
        {
            await _currencyService.DeleteCurrencyAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
