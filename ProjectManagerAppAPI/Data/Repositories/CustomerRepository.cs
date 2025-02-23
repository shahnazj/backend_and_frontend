using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<Customer?> UpdateCustomerAsync(int id, Customer customer)
    {
        var existingCustomer = await _context.Customers.FindAsync(id);
        if (existingCustomer != null)
        {
            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            await _context.SaveChangesAsync();
        }
        return existingCustomer;
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<ContactPerson>> GetContactPersonsByCustomerIdAsync(int customerId)
    {
        return await _context.ContactPersons
            .Where(cp => cp.Customers.Any(c => c.Id == customerId))
            .ToListAsync();
    }

    public Task<List<Project>> GetProjectsByCustomerIdAsync(int id)
    {
        return _context.Projects
            .Where(p => p.CustomerId == id)
            .ToListAsync();
    }



    public async Task<bool> AddContactPersonToCustomerAsync(int customerId, int contactPersonId)
    {
        var customer = await _context.Customers.Include(c => c.ContactPersons)
                                               .FirstOrDefaultAsync(c => c.Id == customerId);
        var contactPerson = await _context.ContactPersons.FindAsync(contactPersonId);

        if (customer == null || contactPerson == null)
            return false;

        if (!customer.ContactPersons.Any(cp => cp.Id == contactPersonId))
        {
            customer.ContactPersons.Add(contactPerson);
            await _context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> RemoveContactPersonFromCustomerAsync(int customerId, int contactPersonId)
    {
        var customer = await _context.Customers.Include(c => c.ContactPersons)
                                               .FirstOrDefaultAsync(c => c.Id == customerId);
        if (customer == null)
            return false;

        var contactPerson = customer.ContactPersons.FirstOrDefault(cp => cp.Id == contactPersonId);
        if (contactPerson == null)
            return false;

        customer.ContactPersons.Remove(contactPerson);
        await _context.SaveChangesAsync();
        return true;
    }
}
