using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;
namespace ProjectManagerAppAPI.Data.Services;


public interface IContactPersonService
{
    Task<List<ContactPersonDTO>> GetAllContactPersonsAsync();
    Task<ContactPersonDTO?> GetContactPersonByIdAsync(int id);
    Task<ContactPersonDTO> CreateContactPersonAsync(CreateContactPersonDTO createContactPersonDTO);
    Task<ContactPersonDTO?> UpdateContactPersonAsync(int id, ContactPersonDTO contactPersonDTO);
    Task<List<CustomerDTO>> GetCustomersByContactPersonIdAsync(int id);
    Task DeleteContactPersonAsync(int id);

    Task<bool> AddCustomerToContactPersonAsync(int contactPersonId, int customerId);
    Task<bool> RemoveCustomerFromContactPersonAsync(int contactPersonId, int customerId);
}
