using ProjectManagerAppAPI.Data.Repositories;
using ProjectManagerAppAPI.DTOs;
using ProjectManagerAppAPI.Mappers;

namespace ProjectManagerAppAPI.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<CurrencyDTO> CreateCurrencyAsync(CreateCurrencyDTO createCurrencyDto)
    {
        if (string.IsNullOrWhiteSpace(createCurrencyDto.Name))
        {
            throw new ArgumentException("Currency name cannot be empty.");
        }

        var currency = CurrencyDTOMapper.ToCurrency(createCurrencyDto);
        var createdCurrency = await _currencyRepository.CreateCurrencyAsync(currency);
        return CurrencyDTOMapper.ToCurrencyDTO(createdCurrency);
    }

    public async Task<bool> DeleteCurrencyAsync(int id)
    {
        var existingCurrency = await _currencyRepository.GetCurrencyByIdAsync(id);
        if (existingCurrency == null)
        {
            throw new KeyNotFoundException($"Currency with ID {id} not found.");
        }
        bool result = await _currencyRepository.DeleteCurrencyAsync(id);
        return result;

    }

    public async Task<List<CurrencyDTO>> GetAllCurrenciesAsync()
    {
        var currencies = await _currencyRepository.GetAllCurrenciesAsync();
        return currencies.Select(CurrencyDTOMapper.ToCurrencyDTO).ToList();
    }

    public async Task<CurrencyDTO?> GetCurrencyByIdAsync(int id)
    {
        var currency = await _currencyRepository.GetCurrencyByIdAsync(id);
        if (currency == null)
        {
            throw new KeyNotFoundException($"Currency with ID {id} not found.");
        }
        return CurrencyDTOMapper.ToCurrencyDTO(currency);
    }
}