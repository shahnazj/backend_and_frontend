using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class UnitDTOMapper
    {
        public static UnitDTO ToUnitDTO(Unit unit)
        {
            return new UnitDTO
            {
                Id = unit.Id,
                Name = unit.Name,
            };
        }
        public static Unit ToUnit(UnitDTO unitDTO)
        {
            return new Unit
            {
                Id = unitDTO.Id,
                Name = unitDTO.Name,
            };
        }
        public static Unit ToUnit(CreateUnitDTO createUnitDTO)
        {
            return new Unit
            {
                Name = createUnitDTO.Name,
            };
        }
    }
}