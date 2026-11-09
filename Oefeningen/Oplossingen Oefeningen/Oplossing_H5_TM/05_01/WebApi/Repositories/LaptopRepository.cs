using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories;

public class LaptopRepository : ILaptopRepository
{
    private readonly LaptopCatalogusContext _context;

    public LaptopRepository(LaptopCatalogusContext context)
    {
        _context = context;
    }

    public async Task<Laptop> CreateAsync(Laptop laptop)
    {
        _context.Laptops.Add(laptop);
        await _context.SaveChangesAsync();

        return laptop;
    }

    public async Task<List<Laptop>> GetAllAsync()
    {
        return await _context.Laptops
            .OrderBy(x => x.Merk)
            .ToListAsync();
    }

    public async Task<Laptop?> GetByIdAsync(int id)
    {
        return await _context.Laptops.FindAsync(id);
    }

    public async Task UpdateAsync(int id, Laptop laptop)
    {
        var existingLaptop = await _context.Laptops.FindAsync(id);

        if (existingLaptop != null)
        {
            existingLaptop.Merk = laptop.Merk;
            existingLaptop.Processor = laptop.Processor;
            existingLaptop.RamInGB = laptop.RamInGB;
            existingLaptop.Prijs = laptop.Prijs;
            existingLaptop.GPU = laptop.GPU;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var laptopToDelete = await _context.Laptops.FindAsync(id);

        if (laptopToDelete != null)
        {
            _context.Laptops.Remove(laptopToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
