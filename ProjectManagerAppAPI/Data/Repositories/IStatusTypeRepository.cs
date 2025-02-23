using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public interface IStatusTypeRepository
{
    Task<List<StatusType>> GetAllStatusTypesAsync();
    Task<StatusType?> GetStatusTypeByIdAsync(int id);
    Task<StatusType> CreateStatusTypeAsync(StatusType statusType);
    Task<StatusType?> UpdateStatusTypeAsync(int id, StatusType statusType);
    Task DeleteStatusTypeAsync(int id);
}
