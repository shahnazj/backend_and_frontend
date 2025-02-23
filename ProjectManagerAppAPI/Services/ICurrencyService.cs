using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Services;
public interface ICurrencyService
{
    Task<List<CurrencyDTO>> GetAllCurrenciesAsync();
    Task<CurrencyDTO?> GetCurrencyByIdAsync(int id);
    Task<CurrencyDTO> CreateCurrencyAsync(CreateCurrencyDTO createCurrencyDTO);
    Task<bool> DeleteCurrencyAsync(int id);
}
