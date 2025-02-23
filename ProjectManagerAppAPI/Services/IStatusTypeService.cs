using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;
public interface IStatusTypeService
{
    Task<List<StatusTypeDTO>> GetAllStatusTypesAsync();
    Task<StatusTypeDTO?> GetStatusTypeByIdAsync(int id);
    Task<StatusTypeDTO> CreateStatusTypeAsync(CreateStatusTypeDTO createStatusTypeDTO);
    Task<StatusTypeDTO?> UpdateStatusTypeAsync(int id, StatusTypeDTO statusTypeDto);
    Task DeleteStatusTypeAsync(int id);
}
