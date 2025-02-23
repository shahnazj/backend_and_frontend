using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;
public interface IUnitService
{
    Task<List<UnitDTO>> GetAllUnitsAsync();
    Task<UnitDTO?> GetUnitByIdAsync(int id);
    Task<UnitDTO> CreateUnitAsync(CreateUnitDTO createUnitDTO);
    Task<bool> DeleteUnitAsync(int id);
}
