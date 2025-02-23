using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class ContactPersonRepository : IContactPersonRepository
{
    private readonly ApplicationDbContext _context;

    public ContactPersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<ContactPerson>> GetAllContactPersonsAsync()
    {
        return await _context.ContactPersons.ToListAsync();
    }
    public async Task<ContactPerson?> GetContactPersonByIdAsync(int id)
    {
        return await _context.ContactPersons.FindAsync(id);
    }

    public async Task<ContactPerson?> UpdateContactPersonAsync(int id, ContactPerson contactPerson)
    {
        var existingContactPerson = await _context.ContactPersons.FindAsync(id);
        if (existingContactPerson != null)
        {
            existingContactPerson.FirstName = contactPerson.FirstName;
            existingContactPerson.LastName = contactPerson.LastName;
            existingContactPerson.Email = contactPerson.Email;
            existingContactPerson.PhoneNumber = contactPerson.PhoneNumber;
            await _context.SaveChangesAsync();
        }
        return existingContactPerson;
    }
    public async Task<List<Customer>> GetCustomersByContactPersonIdAsync(int id)
    {
        return await _context.Customers
            .Where(c => c.ContactPersons.Any(cp => cp.Id == id))
            .ToListAsync();
    }


    public async Task DeleteContactPersonAsync(int id)
    {
        var contactPerson = await _context.ContactPersons.FindAsync(id);
        if (contactPerson != null)
        {
            _context.ContactPersons.Remove(contactPerson);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<ContactPerson> CreateContactPersonAsync(ContactPerson contactPerson)
    {
        _context.ContactPersons.Add(contactPerson);
        await _context.SaveChangesAsync();
        return contactPerson;
    }

    public async Task<bool> AddCustomerToContactPersonAsync(int contactPersonId, int customerId)
    {
        var contactPerson = await _context.ContactPersons.Include(c => c.Customers)
                                               .FirstOrDefaultAsync(c => c.Id == contactPersonId);
        var customer = await _context.Customers.FindAsync(customerId);

        if (contactPerson == null || customer == null)
            return false;

        if (!contactPerson.Customers.Any(c => c.Id == customerId))
        {
            contactPerson.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        return true;
    }




    public async Task<bool> RemoveCustomerFromContactPersonAsync(int contactPersonId, int customerId)
    {
        var contactPerson = await _context.ContactPersons.Include(c => c.Customers)
                                               .FirstOrDefaultAsync(c => c.Id == contactPersonId);
        if (contactPerson == null)
            return false;

        var customer = contactPerson.Customers.FirstOrDefault(c => c.Id == customerId);
        if (customer == null)
            return false;

        contactPerson.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }

}
