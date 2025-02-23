using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class ContactPersonDTOMapper
{
    public static ContactPersonDTO ToContactPersonDTO(ContactPerson contactPerson)
    {
        return new ContactPersonDTO
        {
            Id = contactPerson.Id,
            FirstName = contactPerson.FirstName,
            LastName = contactPerson.LastName,
            Email = contactPerson.Email,
            PhoneNumber = contactPerson.PhoneNumber,
        };
    }

    public static ContactPerson ToContactPerson(CreateContactPersonDTO dto)
    {
        return new ContactPerson
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
    }

    public static ContactPerson ToContactPerson(ContactPersonDTO dto)
    {
        return new ContactPerson
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
    }
}
}