using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;


namespace ProjectManagerAppAPI.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _ServiceRepository;

    public ServiceService(IServiceRepository ServiceRepository)
    {
        _ServiceRepository = ServiceRepository;
    }

    public async Task<List<ServiceDTO>> GetAllServicesAsync()
    {
        var Services = await _ServiceRepository.GetAllServicesAsync();
        return Services.Select(ServiceDTOMapper.ToServiceDTO).ToList();
    }

    public async Task<ServiceDTO?> GetServiceByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Service ID must be greater than zero.");
        }

        var Service = await _ServiceRepository.GetServiceByIdAsync(id);
        if (Service == null)
        {
            throw new KeyNotFoundException($"Service with ID {id} not found.");
        }

        return ServiceDTOMapper.ToServiceDTO(Service);
    }

    public async Task<ServiceDTO> CreateServiceAsync(CreateServiceDTO createServiceDTO)
    {
        if (string.IsNullOrWhiteSpace(createServiceDTO.ServiceName))
        {
            throw new ArgumentException("Service name cannot be empty.");
        }

        var Service = ServiceDTOMapper.ToService(createServiceDTO);
        var createdService = await _ServiceRepository.CreateServiceAsync(Service);

        return ServiceDTOMapper.ToServiceDTO(createdService);
    }

    public async Task<ServiceDTO?> UpdateServiceAsync(int id, ServiceDTO ServiceDTO)
    {
        Console.WriteLine("We are here");
        Console.WriteLine(ServiceDTO.CurrencyId);
        if (id <= 0)
        {
            throw new ArgumentException("Service ID must be greater than zero.");
        }

        var existingService = await _ServiceRepository.GetServiceByIdAsync(id);
        if (existingService == null)
        {
            throw new KeyNotFoundException($"Service with ID {id} not found.");
        }

        if (!string.IsNullOrWhiteSpace(ServiceDTO.ServiceName))
        {
            existingService.ServiceName = ServiceDTO.ServiceName;
        }

        Console.WriteLine(existingService.CurrencyId);

        var updatedService = await _ServiceRepository.UpdateServiceAsync(id, ServiceDTOMapper.ToService(ServiceDTO));
        Console.WriteLine(existingService.CurrencyId);
        if (updatedService == null)
        {
            throw new InvalidOperationException("Failed to update Service.");
        }

        return ServiceDTOMapper.ToServiceDTO(updatedService);
    }


    public async Task DeleteServiceAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Service ID must be greater than zero.");
        }

        var existingService = await _ServiceRepository.GetServiceByIdAsync(id);
        if (existingService == null)
        {
            throw new KeyNotFoundException($"Service with ID {id} not found.");
        }

        await _ServiceRepository.DeleteServiceAsync(id); // No return value, just call the method
    }
}
