using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;

public interface IContactPersonRepository
{
    Task<List<ContactPerson>> GetAllContactPersonsAsync();
    Task<ContactPerson?> GetContactPersonByIdAsync(int id);
    Task<ContactPerson> CreateContactPersonAsync(ContactPerson contactPerson);
    Task<ContactPerson?> UpdateContactPersonAsync(int id, ContactPerson contactPerson);
    Task<List<Customer>> GetCustomersByContactPersonIdAsync(int id);
    Task DeleteContactPersonAsync(int id);

    Task<bool> AddCustomerToContactPersonAsync(int contactPersonId, int customerId);
    Task<bool> RemoveCustomerFromContactPersonAsync(int contactPersonId, int customerId);
}
