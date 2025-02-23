using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class ServiceDTOMapper
    {
        public static ServiceDTO ToServiceDTO(Service service)
        {
            return new ServiceDTO
            {
                Id = service.Id,
                ServiceName = service.ServiceName,
                Price = service.Price,
                UnitId = service.UnitId,
                CurrencyId = service.CurrencyId,
            };
        }

        public static Service ToService(ServiceDTO serviceDTO)
        {
            return new Service
            {
                Id = serviceDTO.Id,
                ServiceName = serviceDTO.ServiceName,
                Price = serviceDTO.Price,
                UnitId = serviceDTO.UnitId,
                CurrencyId = serviceDTO.CurrencyId,
            };
        }

        public static Service ToService(CreateServiceDTO createServiceDTO)
        {
            return new Service
            {
                ServiceName = createServiceDTO.ServiceName,
                Price = createServiceDTO.Price,
                UnitId = createServiceDTO.UnitId,
                CurrencyId = createServiceDTO.CurrencyId,
            };
        }

    }
}