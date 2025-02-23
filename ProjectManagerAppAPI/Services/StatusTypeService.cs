using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Services;
public class StatusTypeService : IStatusTypeService
{
    private readonly IStatusTypeRepository _statusTypeRepository;

    public StatusTypeService(IStatusTypeRepository statusTypeRepository)
    {
        _statusTypeRepository = statusTypeRepository;
    }

    public async Task<List<StatusTypeDTO>> GetAllStatusTypesAsync()
    {
        var statusTypes = await _statusTypeRepository.GetAllStatusTypesAsync();
        return statusTypes.Select(StatusTypeDTOMapper.ToStatusTypeDTO).ToList();
    }

    public async Task<StatusTypeDTO?> GetStatusTypeByIdAsync(int id)
    {
        var statusType = await _statusTypeRepository.GetStatusTypeByIdAsync(id);
        return statusType == null ? null : StatusTypeDTOMapper.ToStatusTypeDTO(statusType);
    }

    public async Task<StatusTypeDTO> CreateStatusTypeAsync(CreateStatusTypeDTO createStatusTypeDTO)
    {
        if (string.IsNullOrWhiteSpace(createStatusTypeDTO.Status))
        {
            throw new ArgumentException("Status cannot be empty.");
        }

        var statusType = StatusTypeDTOMapper.ToStatusType(createStatusTypeDTO);
        var createdStatusType = await _statusTypeRepository.CreateStatusTypeAsync(statusType);

        return StatusTypeDTOMapper.ToStatusTypeDTO(createdStatusType);
    }

    public async Task<StatusTypeDTO?> UpdateStatusTypeAsync(int id, StatusTypeDTO statusTypeDto)
    {
        if (string.IsNullOrWhiteSpace(statusTypeDto.Status))
        {
            throw new ArgumentException("Status cannot be empty.");
        }

        var existingStatusType = await _statusTypeRepository.GetStatusTypeByIdAsync(id);
        if (existingStatusType == null)
        {
            return null;
        }

        existingStatusType.Status = statusTypeDto.Status;
        var updatedStatusType = await _statusTypeRepository.UpdateStatusTypeAsync(id, StatusTypeDTOMapper.ToStatusType(statusTypeDto));

        return updatedStatusType == null ? null : StatusTypeDTOMapper.ToStatusTypeDTO(updatedStatusType);
    }

    public async Task DeleteStatusTypeAsync(int id)
    {
        await _statusTypeRepository.DeleteStatusTypeAsync(id);
    }
}
