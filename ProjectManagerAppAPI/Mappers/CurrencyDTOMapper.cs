using ProjectManagerAppAPI.Models;
using ProjectManagerAppAPI.DTOs;

namespace ProjectManagerAppAPI.Mappers
{
    public static class CurrencyDTOMapper
    {
        public static CurrencyDTO ToCurrencyDTO(Currency currency)
        {
            return new CurrencyDTO
            {
                Id = currency.Id,
                Name = currency.Name,
            };
        }

        public static Currency ToCurrency(CurrencyDTO currencyDTO)
        {
            return new Currency
            {
                Id = currencyDTO.Id,
                Name = currencyDTO.Name,
            };
        }

        public static Currency ToCurrency(CreateCurrencyDTO createCurrencyDTO)
        {
            return new Currency
            {
                Name = createCurrencyDTO.Name,
            };
        }

    }
}