using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;


public interface IServiceRepository
{
    Task<List<Service>> GetAllServicesAsync();
    Task<Service?> GetServiceByIdAsync(int id);
    Task<Service> CreateServiceAsync(Service Service);
    Task<Service?> UpdateServiceAsync(int id, Service Service);
    Task DeleteServiceAsync(int id);
}
