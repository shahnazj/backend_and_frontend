using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class EmployeeDTOMapper
    {

        public static EmployeeDTO ToEmployeeDTO(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                RoleId = employee.RoleId,
            };
        }
        public static Employee ToEmployee(EmployeeDTO employeeDTO)
        {
            return new Employee
            {
                Id = employeeDTO.Id,
                FirstName = employeeDTO.FirstName,
                LastName = employeeDTO.LastName,
                RoleId = employeeDTO.RoleId,

            };
        }

        public static Employee ToEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            return new Employee
            {
                FirstName = createEmployeeDTO.FirstName,
                LastName = createEmployeeDTO.LastName,
                RoleId = createEmployeeDTO.RoleId,
            };
        }
    }
}