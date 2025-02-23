using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.Data.Services;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;

namespace ProjectManagerAppAPI.Services;

public class ContactPersonService : IContactPersonService
{
    private readonly IContactPersonRepository _contactPersonRepository;

    public ContactPersonService(IContactPersonRepository contactPersonRepository)
    {
        _contactPersonRepository = contactPersonRepository;
    }

    public async Task<List<ContactPersonDTO>> GetAllContactPersonsAsync()
    {
        var contactPersons = await _contactPersonRepository.GetAllContactPersonsAsync();
        return contactPersons.Select(ContactPersonDTOMapper.ToContactPersonDTO).ToList();
    }

    public async Task<ContactPersonDTO?> GetContactPersonByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Contact person ID must be greater than zero.");
        }

        var contactPerson = await _contactPersonRepository.GetContactPersonByIdAsync(id);
        if (contactPerson == null)
        {
            throw new KeyNotFoundException($"Contact person with ID {id} not found.");
        }

        return ContactPersonDTOMapper.ToContactPersonDTO(contactPerson);
    }

    public async Task<ContactPersonDTO> CreateContactPersonAsync(CreateContactPersonDTO createContactPersonDTO)
    {
        if (string.IsNullOrWhiteSpace(createContactPersonDTO.FirstName) || string.IsNullOrWhiteSpace(createContactPersonDTO.LastName))
        {
            throw new ArgumentException("Contact person First and Last name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(createContactPersonDTO.Email) || !createContactPersonDTO.Email.Contains("@"))
        {
            throw new ArgumentException("Invalid email format.");
        }

        var contactPerson = ContactPersonDTOMapper.ToContactPerson(createContactPersonDTO);
        var createdContactPerson = await _contactPersonRepository.CreateContactPersonAsync(contactPerson);

        return ContactPersonDTOMapper.ToContactPersonDTO(createdContactPerson);
    }



    public async Task<ContactPersonDTO?> UpdateContactPersonAsync(int id, ContactPersonDTO contactPersonDTO)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Contact person ID must be greater than zero.");
        }

        var existingContactPerson = await _contactPersonRepository.GetContactPersonByIdAsync(id);
        if (existingContactPerson == null)
        {
            throw new KeyNotFoundException($"Contact person with ID {id} not found.");
        }

        if (!string.IsNullOrWhiteSpace(contactPersonDTO.FirstName))
        {
            existingContactPerson.FirstName = contactPersonDTO.FirstName;
        }
        if (!string.IsNullOrWhiteSpace(contactPersonDTO.LastName))
        {
            existingContactPerson.LastName = contactPersonDTO.LastName;
        }

        if (!string.IsNullOrWhiteSpace(contactPersonDTO.Email) && contactPersonDTO.Email.Contains("@"))
        {
            existingContactPerson.Email = contactPersonDTO.Email;
        }

        var updatedContactPerson = await _contactPersonRepository.UpdateContactPersonAsync(id, ContactPersonDTOMapper.ToContactPerson(contactPersonDTO));
        if (updatedContactPerson == null)
        {
            throw new InvalidOperationException("Failed to update contact person.");
        }

        return ContactPersonDTOMapper.ToContactPersonDTO(updatedContactPerson);
    }

    public async Task DeleteContactPersonAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Contact person ID must be greater than zero.");
        }

        var existingContactPerson = await _contactPersonRepository.GetContactPersonByIdAsync(id);
        if (existingContactPerson == null)
        {
            throw new KeyNotFoundException($"Contact person with ID {id} not found.");
        }

        await _contactPersonRepository.DeleteContactPersonAsync(id);
    }

    public async Task<List<CustomerDTO>> GetCustomersByContactPersonIdAsync(int id)
    {
        var customers = await _contactPersonRepository.GetCustomersByContactPersonIdAsync(id);
        return customers.Select(CustomerDTOMapper.ToCustomerDTO).ToList();
    }

    public async Task<bool> AddCustomerToContactPersonAsync(int contactPersonId, int customerId)
    {
        return await _contactPersonRepository.AddCustomerToContactPersonAsync(contactPersonId, customerId);
    }

    public async Task<bool> RemoveCustomerFromContactPersonAsync(int contactPersonId, int customerId)
    {
        return await _contactPersonRepository.RemoveCustomerFromContactPersonAsync(contactPersonId, customerId);
    }
}
