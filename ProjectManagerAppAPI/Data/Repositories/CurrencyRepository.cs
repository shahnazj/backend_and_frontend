using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;

    public async Task<List<Currency>> GetAllCurrenciesAsync()
    {
        return await _context.Currencies.ToListAsync();
    }
    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Currency> CreateCurrencyAsync(Currency currency)
    {
        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();
        return currency;
    }

    public async Task<bool> DeleteCurrencyAsync(int id)
    {
        var currency = await _context.Currencies.FindAsync(id);
        if (currency != null)
        {
            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<Currency?> GetCurrencyByIdAsync(int id)
    {
        return await _context.Currencies.FindAsync(id);
    }

    public async Task<Currency?> UpdateCurrencyAsync(int id, Currency currency)
    {
        var existingCurrency = await _context.Currencies.FindAsync(id);
        if (existingCurrency == null)
        {
            return null;
        }
        existingCurrency.Name = currency.Name;
        await _context.SaveChangesAsync();
        return existingCurrency;
    }
}