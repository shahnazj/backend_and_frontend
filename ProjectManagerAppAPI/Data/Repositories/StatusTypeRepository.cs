using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class StatusTypeRepository : IStatusTypeRepository
{
    private readonly ApplicationDbContext _context;

    public StatusTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StatusType>> GetAllStatusTypesAsync()
    {
        return await _context.StatusTypes.ToListAsync();
    }

    public async Task<StatusType?> GetStatusTypeByIdAsync(int id)
    {
        return await _context.StatusTypes.FindAsync(id);
    }

    public async Task<StatusType> CreateStatusTypeAsync(StatusType statusType)
    {
        _context.StatusTypes.Add(statusType);
        await _context.SaveChangesAsync();
        return statusType;
    }

    public async Task<StatusType?> UpdateStatusTypeAsync(int id, StatusType statusType)
    {
        var existingStatus = await _context.StatusTypes.FindAsync(id);
        if (existingStatus != null)
        {
            existingStatus.Status = statusType.Status;
            await _context.SaveChangesAsync();
        }
        return existingStatus;
    }

    public async Task DeleteStatusTypeAsync(int id)
    {
        var statusType = await _context.StatusTypes.FindAsync(id);
        if (statusType != null)
        {
            _context.StatusTypes.Remove(statusType);
            await _context.SaveChangesAsync();
        }
    }
}
