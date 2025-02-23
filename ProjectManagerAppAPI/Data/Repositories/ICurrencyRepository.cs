using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public interface ICurrencyRepository
{
    Task<List<Currency>> GetAllCurrenciesAsync();
    Task<Currency?> GetCurrencyByIdAsync(int id);
    Task<Currency?> UpdateCurrencyAsync(int id, Currency currency);
    Task<Currency> CreateCurrencyAsync(Currency currency);
    Task<bool> DeleteCurrencyAsync(int id);
}
