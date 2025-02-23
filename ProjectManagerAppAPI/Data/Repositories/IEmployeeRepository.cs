using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee?> GetEmployeeByIdAsync(int id);
    Task<Employee> CreateEmployeeAsync(Employee employee);
    Task<Employee?> UpdateEmployeeAsync(int id, Employee employee);
    Task DeleteEmployeeAsync(int id);
}
