using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;

public interface IServiceService
{
    Task<List<ServiceDTO>> GetAllServicesAsync();
    Task<ServiceDTO?> GetServiceByIdAsync(int id);
    Task<ServiceDTO> CreateServiceAsync(CreateServiceDTO createServiceDTO);
    Task<ServiceDTO?> UpdateServiceAsync(int id, ServiceDTO ServiceDTO);
    Task DeleteServiceAsync(int id);
}