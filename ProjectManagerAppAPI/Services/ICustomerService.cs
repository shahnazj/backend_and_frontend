using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetAllCustomersAsync();
    Task<CustomerDTO?> GetCustomerByIdAsync(int id);
    Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO);
    Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO CustomerDTO);
    Task<List<ContactPersonDTO>> GetContactPersonsByCustomerIdAsync(int id);
    Task DeleteCustomerAsync(int id);

    Task<bool> AddContactPersonToCustomerAsync(int customerId, int contactPersonId);
    Task<bool> RemoveContactPersonFromCustomerAsync(int customerId, int contactPersonId);

}