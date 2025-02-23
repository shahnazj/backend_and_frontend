using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class StatusTypeDTOMapper
    {
        public static StatusTypeDTO ToStatusTypeDTO(StatusType statusType)
        {
            return new StatusTypeDTO
            {
                Id = statusType.Id,
                Status = statusType.Status,
            };
        }
        public static StatusType ToStatusType(StatusTypeDTO statusTypeDTO)
        {
            return new StatusType
            {
                Id = statusTypeDTO.Id,
                Status = statusTypeDTO.Status,
            };
        }

        public static StatusType ToStatusType(CreateStatusTypeDTO createStatusTypeDTO)
        {
            return new StatusType
            {
                Status = createStatusTypeDTO.Status,
            };
        }

    }
}