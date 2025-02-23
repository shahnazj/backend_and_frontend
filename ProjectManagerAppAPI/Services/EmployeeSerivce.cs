using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;

namespace ProjectManagerAppAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    public async Task<List<EmployeeDTO>> GetAllEmployeesAsync()
    {
        var employees = await _employeeRepository.GetAllEmployeesAsync();
        return employees.Select(EmployeeDTOMapper.ToEmployeeDTO).ToList();
    }

    public async Task<EmployeeDTO?> GetEmployeeByIdAsync(int id)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }
        return EmployeeDTOMapper.ToEmployeeDTO(employee);
    }

    public async Task<EmployeeDTO> CreateEmployeeAsync(CreateEmployeeDTO createEmployeeDTO)
    {
        if (string.IsNullOrWhiteSpace(createEmployeeDTO.FirstName) || string.IsNullOrWhiteSpace(createEmployeeDTO.LastName))
        {
            throw new ArgumentException("First name and last name are required.");
        }

        var employee = EmployeeDTOMapper.ToEmployee(createEmployeeDTO);
        var createdEmployee = await _employeeRepository.CreateEmployeeAsync(employee);
        return EmployeeDTOMapper.ToEmployeeDTO(createdEmployee);
    }

    public async Task<EmployeeDTO?> UpdateEmployeeAsync(int id, EmployeeDTO employeeDto)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (existingEmployee == null)
        {
            throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }

        var updatedEmployee = EmployeeDTOMapper.ToEmployee(employeeDto);
        var result = await _employeeRepository.UpdateEmployeeAsync(id, EmployeeDTOMapper.ToEmployee(employeeDto));
        return result != null ? EmployeeDTOMapper.ToEmployeeDTO(result) : null;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
        if (existingEmployee == null)
        {
            throw new KeyNotFoundException($"Employee with ID {id} not found.");
        }
        await _employeeRepository.DeleteEmployeeAsync(id);
    }
}
