using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;

namespace ProjectManagerAppAPI.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public async Task<UnitDTO> CreateUnitAsync(CreateUnitDTO createUnitDto)
    {
        if (string.IsNullOrWhiteSpace(createUnitDto.Name))
        {
            throw new ArgumentException("Unit name cannot be empty.");
        }

        var unit = UnitDTOMapper.ToUnit(createUnitDto);
        var createdUnit = await _unitRepository.CreateUnitAsync(unit);
        return UnitDTOMapper.ToUnitDTO(createdUnit);
    }

    public async Task<bool> DeleteUnitAsync(int id)
    {
        var existingUnit = await _unitRepository.GetUnitByIdAsync(id);
        if (existingUnit == null)
        {
            throw new KeyNotFoundException($"Unit with ID {id} not found.");
        }
        bool result = await _unitRepository.DeleteUnitAsync(id);
        return result;

    }

    public async Task<List<UnitDTO>> GetAllUnitsAsync()
    {
        var units = await _unitRepository.GetAllUnitsAsync();
        return units.Select(UnitDTOMapper.ToUnitDTO).ToList();
    }

    public async Task<UnitDTO?> GetUnitByIdAsync(int id)
    {
        var unit = await _unitRepository.GetUnitByIdAsync(id);
        if (unit == null)
        {
            throw new KeyNotFoundException($"Unit with ID {id} not found.");
        }
        return UnitDTOMapper.ToUnitDTO(unit);
    }
}