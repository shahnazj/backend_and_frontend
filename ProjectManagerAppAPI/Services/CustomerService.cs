using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;


namespace ProjectManagerAppAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<List<CustomerDTO>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllCustomersAsync();
        return customers.Select(CustomerDTOMapper.ToCustomerDTO).ToList();
    }

    public async Task<CustomerDTO?> GetCustomerByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Customer ID must be greater than zero.");
        }

        var customer = await _customerRepository.GetCustomerByIdAsync(id);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }

        return CustomerDTOMapper.ToCustomerDTO(customer);
    }

    public async Task<CustomerDTO> CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
    {
        if (string.IsNullOrWhiteSpace(createCustomerDTO.Name))
        {
            throw new ArgumentException("Customer name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(createCustomerDTO.Email) || !createCustomerDTO.Email.Contains("@"))
        {
            throw new ArgumentException("Invalid email format.");
        }

        var customer = CustomerDTOMapper.ToCustomer(createCustomerDTO);
        var createdCustomer = await _customerRepository.CreateCustomerAsync(customer);

        return CustomerDTOMapper.ToCustomerDTO(createdCustomer);
    }

    public async Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO customerDTO)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Customer ID must be greater than zero.");
        }

        var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
        if (existingCustomer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }

        if (!string.IsNullOrWhiteSpace(customerDTO.Name))
        {
            existingCustomer.Name = customerDTO.Name;
        }

        if (!string.IsNullOrWhiteSpace(customerDTO.Email) && customerDTO.Email.Contains("@"))
        {
            existingCustomer.Email = customerDTO.Email;
        }
        Console.WriteLine(existingCustomer.PhoneNumber);
        var updatedCustomer = await _customerRepository.UpdateCustomerAsync(id, CustomerDTOMapper.ToCustomer(customerDTO));
        Console.WriteLine(updatedCustomer.PhoneNumber);
        // Ensure we handle the case where the update fails and returns null
        if (updatedCustomer == null)
        {
            throw new InvalidOperationException("Failed to update customer.");
        }

        return CustomerDTOMapper.ToCustomerDTO(updatedCustomer);
    }


    public async Task DeleteCustomerAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Customer ID must be greater than zero.");
        }

        var existingCustomer = await _customerRepository.GetCustomerByIdAsync(id);
        if (existingCustomer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }

        await _customerRepository.DeleteCustomerAsync(id); // No return value, just call the method
    }

    public async Task<List<ContactPersonDTO>> GetContactPersonsByCustomerIdAsync(int id)
    {
        var contactPersons = await _customerRepository.GetContactPersonsByCustomerIdAsync(id);

        return contactPersons.Select(ContactPersonDTOMapper.ToContactPersonDTO).ToList();
    }

    public async Task<bool> AddContactPersonToCustomerAsync(int customerId, int contactPersonId)
    {
        return await _customerRepository.AddContactPersonToCustomerAsync(customerId, contactPersonId);
    }

    public async Task<bool> RemoveContactPersonFromCustomerAsync(int customerId, int contactPersonId)
    {
        return await _customerRepository.RemoveContactPersonFromCustomerAsync(customerId, contactPersonId);
    }
}
