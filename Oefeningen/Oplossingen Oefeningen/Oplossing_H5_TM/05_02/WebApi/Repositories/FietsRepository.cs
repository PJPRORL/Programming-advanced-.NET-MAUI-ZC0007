using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories;

public class FietsRepository : IFietsRepository
{
    private readonly FietsCatalogusContext _context;

    public FietsRepository(FietsCatalogusContext context)
    {
        _context = context;
    }

    public async Task<Fiets> CreateAsync(Fiets fiets)
    {
        _context.Fietsen.Add(fiets);
        await _context.SaveChangesAsync();

        return fiets;
    }

    public async Task<List<Fiets>> GetAllAsync()
    {
        return await _context.Fietsen
            .OrderBy(x => x.Merk)
            .ToListAsync();
    }

    public async Task<Fiets?> GetByIdAsync(int id)
    {
        return await _context.Fietsen.FindAsync(id);
    }

    public async Task UpdateAsync(int id, Fiets fiets)
    {
        var existingFiets = await _context.Fietsen.FindAsync(id);

        if (existingFiets != null)
        {
            existingFiets.Merk = fiets.Merk;
            existingFiets.Type = fiets.Type;
            existingFiets.Kleur = fiets.Kleur;
            existingFiets.Prijs = fiets.Prijs;
            existingFiets.Aantal = fiets.Aantal;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var fietsToDelete = await _context.Fietsen.FindAsync(id);

        if (fietsToDelete != null)
        {
            _context.Fietsen.Remove(fietsToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Fiets>> GetByMerkAsync(string merk)
    {
        return await _context.Fietsen
            .Where(x => x.Merk.ToLower() == merk.ToLower())
            .OrderBy(x => x.Prijs)
            .ToListAsync();
    }

    public async Task<List<Fiets>> GetByTypeAsync(string type)
    {
        return await _context.Fietsen
            .Where(x => x.Type.ToLower() == type.ToLower())
            .OrderBy(x => x.Prijs)
            .ToListAsync();
    }

    public async Task<List<Fiets>> GetByKleurAsync(string kleur)
    {
        return await _context.Fietsen
            .Where(x => x.Kleur.ToLower() == kleur.ToLower())
            .OrderBy(x => x.Prijs)
            .ToListAsync();
    }

    public async Task<List<Fiets>> GetInStockAsync()
    {
        return await _context.Fietsen
            .Where(x => x.Aantal > 0)
            .OrderBy(x => x.Prijs)
            .ToListAsync();
    }

    public async Task<List<Fiets>> GetSortedByPrijsAsync(bool descending)
    {
        if (descending)
        {
            return await _context.Fietsen
                .OrderByDescending(x => x.Prijs)
                .ToListAsync();
        }
        else
        {
            return await _context.Fietsen
                .OrderBy(x => x.Prijs)
                .ToListAsync();
        }
    }

    public async Task<bool> ExistsByMerkAsync(string merk)
    {
        return await _context.Fietsen
            .AnyAsync(x => x.Merk.ToLower() == merk.ToLower());
    }

    public async Task<int> CountByTypeAsync(string type)
    {
        return await _context.Fietsen
            .CountAsync(x => x.Type.ToLower() == type.ToLower());
    }
}
