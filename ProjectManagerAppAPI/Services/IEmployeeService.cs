using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;

public interface IEmployeeService
{
    Task<List<EmployeeDTO>> GetAllEmployeesAsync();
    Task<EmployeeDTO?> GetEmployeeByIdAsync(int id);
    Task<EmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO);
    Task<EmployeeDTO?> UpdateEmployeeAsync(int id, EmployeeDTO employeeDto);
    Task DeleteEmployeeAsync(int id);
}
