using Microsoft.EntityFrameworkCore;
using ProjectManagerAppAPI.Models;

namespace ProjectManagerAppAPI.Data.Repositories;
public class ServiceRepository : IServiceRepository
{
    private readonly ApplicationDbContext _context;

    public ServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Service>> GetAllServicesAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<Service?> GetServiceByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task<Service> CreateServiceAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        return service;
    }

    public async Task<Service?> UpdateServiceAsync(int id, Service service)
    {
        Console.WriteLine("We are here2");
        Console.WriteLine(service.CurrencyId);
        var existingService = await _context.Services.FindAsync(id);
        if (existingService != null)
        {
            existingService.ServiceName = service.ServiceName;
            existingService.Price = service.Price;
            existingService.CurrencyId = service.CurrencyId;
            existingService.UnitId = service.UnitId;
            Console.WriteLine(existingService.CurrencyId);
            await _context.SaveChangesAsync();

            return existingService;
        }
        return null;

    }

    public async Task DeleteServiceAsync(int id)
    {
        var Service = await _context.Services.FindAsync(id);
        if (Service != null)
        {
            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();
        }
    }
}
