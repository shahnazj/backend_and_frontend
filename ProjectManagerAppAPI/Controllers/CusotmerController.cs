using Microsoft.AspNetCore.Mvc;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Services;

namespace ProjectManagerAppAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            return NotFound($"Customer with ID {id} not found.");
        }

        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CreateCustomerDTO createCustomerDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdCustomerDTO = await _customerService.CreateCustomerAsync(createCustomerDTO);
            return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomerDTO.Id }, createdCustomerDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error creating customer.");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDTO>> UpdateCustomer(int id, [FromBody] CustomerDTO customerDto)
    {
        if (id != customerDto.Id)
        {
            return BadRequest("ID in URL does not match ID in request body.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customerDto);

            if (updatedCustomer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            return Ok(updatedCustomer);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error updating customer.");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        try
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }

            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting customer.");
        }
    }

    [HttpGet("{id}/contactpersons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ContactPersonDTO>>> GetContactPersonsByCustomerId(int id)
    {
        try
        {
            var contactPersons = await _customerService.GetContactPersonsByCustomerIdAsync(id);

            if (contactPersons == null || !contactPersons.Any())
            {
                return NotFound($"No contact persons found for customer ID {id}.");
            }

            return Ok(contactPersons);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving contact persons.");
        }
    }


    [HttpPost("{customerId}/contactpersons/{contactPersonId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddContactPersonToCustomer(int customerId, int contactPersonId)
    {
        var result = await _customerService.AddContactPersonToCustomerAsync(customerId, contactPersonId);

        if (!result)
        {
            return NotFound("Customer or ContactPerson not found.");
        }

        return Ok("ContactPerson added to Customer successfully.");
    }

    [HttpDelete("{customerId}/contactpersons/{contactPersonId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveContactPersonFromCustomer(int customerId, int contactPersonId)
    {
        var result = await _customerService.RemoveContactPersonFromCustomerAsync(customerId, contactPersonId);

        if (!result)
        {
            return NotFound("Customer or ContactPerson not found.");
        }

        return Ok("ContactPerson removed from Customer successfully.");
    }

}
