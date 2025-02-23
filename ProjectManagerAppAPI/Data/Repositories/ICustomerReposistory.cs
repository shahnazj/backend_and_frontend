using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;


public interface ICustomerRepository
{
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer?> GetCustomerByIdAsync(int id);
    Task<Customer> CreateCustomerAsync(Customer customer);
    Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
    Task<List<ContactPerson>> GetContactPersonsByCustomerIdAsync(int id);
    Task<List<Project>> GetProjectsByCustomerIdAsync(int id);
    Task DeleteCustomerAsync(int id);
    Task<bool> AddContactPersonToCustomerAsync(int customerId, int contactPersonId);
    Task<bool> RemoveContactPersonFromCustomerAsync(int customerId, int contactPersonId);
}
