using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public interface IUnitRepository
{
    Task<List<Unit>> GetAllUnitsAsync();
    Task<Unit?> GetUnitByIdAsync(int id);
    Task<Unit?> UpdateUnitAsync(int id, Unit unit);
    Task<Unit> CreateUnitAsync(Unit unit);
    Task<bool> DeleteUnitAsync(int id);
}
