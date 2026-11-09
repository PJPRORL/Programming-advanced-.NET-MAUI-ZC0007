using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories;

public class BestellingRepository : IBestellingRepository
{
    private readonly BestellingContext _context;

    public BestellingRepository(BestellingContext context)
    {
        _context = context;
    }

    public async Task<Bestelling> CreateAsync(Bestelling bestelling)
    {
        int maxId = await _context.Bestellingen.AnyAsync() ? await _context.Bestellingen.MaxAsync(x => x.Id) : 0;
        bestelling.Id = maxId + 1;

        _context.Bestellingen.Add(bestelling);
        await _context.SaveChangesAsync();

        return bestelling;
    }

    public async Task<List<Bestelling>> GetAllAsync()
    {
        return await _context.Bestellingen
            .OrderBy(x => x.Tafelnummer)
            .ToListAsync();
    }

    public async Task<Bestelling?> GetByIdAsync(int id)
    {
        return await _context.Bestellingen.FindAsync(id);
    }

    public async Task UpdateAsync(int id, Bestelling bestelling)
    {
        var existingBestelling = await _context.Bestellingen.FindAsync(id);

        if (existingBestelling != null)
        {
            existingBestelling.KlantNaam = bestelling.KlantNaam;
            existingBestelling.Tafelnummer = bestelling.Tafelnummer;
            existingBestelling.Gerechten = bestelling.Gerechten;
            existingBestelling.Status = bestelling.Status;
            existingBestelling.TotaalPrijs = bestelling.TotaalPrijs;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var bestellingToDelete = await _context.Bestellingen.FindAsync(id);

        if (bestellingToDelete != null)
        {
            _context.Bestellingen.Remove(bestellingToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Bestelling>> GetByStatusAsync(string status)
    {
        return await _context.Bestellingen
            .Where(x => x.Status.ToLower() == status.ToLower())
            .OrderBy(x => x.Tafelnummer)
            .ToListAsync();
    }

    public async Task<List<Bestelling>> GetByTafelAsync(int tafelnnummer)
    {
        return await _context.Bestellingen
            .Where(x => x.Tafelnummer == tafelnnummer)
            .OrderBy(x => x.TotaalPrijs)
            .ToListAsync();
    }

    public async Task<List<Bestelling>> GetSortedByTotaalPrijsAsync(bool descending)
    {
        if (descending)
        {
            return await _context.Bestellingen
                .OrderByDescending(x => x.TotaalPrijs)
                .ToListAsync();
        }
        else
        {
            return await _context.Bestellingen
                .OrderBy(x => x.TotaalPrijs)
                .ToListAsync();
        }
    }

    public async Task<double> GetTotalRevenueAsync()
    {
        var count = await _context.Bestellingen.CountAsync();
        if (count == 0)
            return 0;

        return await _context.Bestellingen.SumAsync(x => x.TotaalPrijs);
    }

    public async Task<double> GetAverageOrderValueAsync()
    {
        var count = await _context.Bestellingen.CountAsync();
        if (count == 0)
            return 0;

        return await _context.Bestellingen.AverageAsync(x => x.TotaalPrijs);
    }

    public async Task<int> CountByStatusAsync(string status)
    {
        return await _context.Bestellingen
            .CountAsync(x => x.Status.ToLower() == status.ToLower());
    }

    public async Task<bool> HasAnyOrderWithStatusAsync(string status)
    {
        return await _context.Bestellingen
            .AnyAsync(x => x.Status.ToLower() == status.ToLower());
    }
}
