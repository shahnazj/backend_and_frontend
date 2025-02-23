using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.Data.Services;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Services;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactPersonsController : ControllerBase
{
    private readonly IContactPersonService _contactPersonService;

    public ContactPersonsController(IContactPersonService contactPersonService)
    {
        _contactPersonService = contactPersonService ?? throw new ArgumentNullException(nameof(contactPersonService));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ContactPersonDTO>>> GetAllContactPersons()
    {
        var contactPersons = await _contactPersonService.GetAllContactPersonsAsync();
        return Ok(contactPersons);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactPersonDTO>> GetContactPersonById(int id)
    {
        var contactPerson = await _contactPersonService.GetContactPersonByIdAsync(id);
        if (contactPerson == null)
        {
            return NotFound($"Contact person with ID {id} not found.");
        }
        return Ok(contactPerson);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactPersonDTO>> CreateContactPerson([FromBody] CreateContactPersonDTO createContactPersonDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdContactPersonDTO = await _contactPersonService.CreateContactPersonAsync(createContactPersonDTO);
        return CreatedAtAction(nameof(GetContactPersonById), new { id = createdContactPersonDTO.Id }, createdContactPersonDTO);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ContactPersonDTO>> UpdateContactPerson(int id, [FromBody] ContactPersonDTO contactPersonDTO)
    {
        if (id != contactPersonDTO.Id)
        {
            return BadRequest("ID in URL does not match ID in request body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedContactPerson = await _contactPersonService.UpdateContactPersonAsync(id, contactPersonDTO);
        if (updatedContactPerson == null)
        {
            return NotFound($"Contact person with ID {id} not found.");
        }

        return Ok(updatedContactPerson);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContactPerson(int id)
    {
        var contactPerson = await _contactPersonService.GetContactPersonByIdAsync(id);
        if (contactPerson == null)
        {
            return NotFound($"Contact person with ID {id} not found.");
        }

        await _contactPersonService.DeleteContactPersonAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/customers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<CustomerDTO>>> GetCustomersByContactPersonId(int id)
    {
        var customers = await _contactPersonService.GetCustomersByContactPersonIdAsync(id);
        if (customers == null || !customers.Any())
        {
            return NotFound($"No customers found for contact person ID {id}.");
        }
        return Ok(customers);
    }

    [HttpPost("{contactPersonId}/customers/{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddCustomerToContactPerson(int contactPersonId, int customerId)
    {
        var result = await _contactPersonService.AddCustomerToContactPersonAsync(contactPersonId, customerId);
        if (!result)
        {
            return NotFound("Contact person or customer not found.");
        }
        return Ok("Customer added to ContactPerson successfully.");
    }

    [HttpDelete("{contactPersonId}/customers/{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveCustomerFromContactPerson(int contactPersonId, int customerId)
    {
        var result = await _contactPersonService.RemoveCustomerFromContactPersonAsync(contactPersonId, customerId);
        if (!result)
        {
            return NotFound("Contact person or customer not found.");
        }
        return Ok("Customer removed from ContactPerson successfully.");
    }
}
