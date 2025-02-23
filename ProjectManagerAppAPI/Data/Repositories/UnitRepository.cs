using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class UnitRepository : IUnitRepository
{
    private readonly ApplicationDbContext _context;

    public UnitRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> CreateUnitAsync(Unit unit)
    {
        _context.Units.Add(unit);
        await _context.SaveChangesAsync();
        return unit;
    }

    public async Task<bool> DeleteUnitAsync(int id)
    {
        var unit = await _context.Units.FindAsync(id);
        if (unit != null)
        {
            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<Unit>> GetAllUnitsAsync()
    {
        return await _context.Units.ToListAsync();
    }

    public async Task<Unit?> GetUnitByIdAsync(int id)
    {
        return await _context.Units.FindAsync(id);
    }

    public async Task<Unit?> UpdateUnitAsync(int id, Unit unit)
    {
        var existingUnit = await _context.Units.FindAsync(id);
        if (existingUnit == null)
        {
            return null;
        }
        existingUnit.Name = unit.Name;

        await _context.SaveChangesAsync();
        return existingUnit;
    }
}