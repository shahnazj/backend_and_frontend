using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class CustomerDTOMapper
    {
        public static CustomerDTO ToCustomerDTO(Customer customer)
        {
            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };
        }
        public static Customer ToCustomer(CustomerDTO customerDTO)
        {
            return new Customer
            {
                Id = customerDTO.Id,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                PhoneNumber = customerDTO.PhoneNumber,
            };
        }
        public static Customer ToCustomer(CreateCustomerDTO createCustomerDTO)
        {
            return new Customer
            {
                Name = createCustomerDTO.Name,
                Email = createCustomerDTO.Email,
                PhoneNumber = createCustomerDTO.PhoneNumber,
            };
        }
    }
}
