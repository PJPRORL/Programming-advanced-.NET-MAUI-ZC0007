using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Repositories;

public class BoekRepository : IBoekRepository
{
    private readonly BoekCatalogusContext _context;

    public BoekRepository(BoekCatalogusContext context)
    {
        _context = context;
    }

    public async Task<Boek> CreateAsync(Boek boek)
    {
        _context.Boeken.Add(boek);
        await _context.SaveChangesAsync();

        return boek;
    }

    public async Task<List<Boek>> GetAllAsync()
    {
        return await _context.Boeken
            .OrderBy(x => x.Auteur)
            .ToListAsync();
    }

    public async Task<Boek?> GetByIdAsync(int id)
    {
        return await _context.Boeken.FindAsync(id);
    }

    public async Task UpdateAsync(int id, Boek boek)
    {
        var existingBoek = await _context.Boeken.FindAsync(id);

        if (existingBoek != null)
        {
            existingBoek.Titel = boek.Titel;
            existingBoek.Auteur = boek.Auteur;
            existingBoek.Genre = boek.Genre;
            existingBoek.Prijs = boek.Prijs;
            existingBoek.Uitgeverij = boek.Uitgeverij;
            existingBoek.Voorraad = boek.Voorraad;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var boekToDelete = await _context.Boeken.FindAsync(id);

        if (boekToDelete != null)
        {
            _context.Boeken.Remove(boekToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<string>> GetUniqueGenresAsync()
    {
        return await _context.Boeken
            .Select(x => x.Genre)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<List<string>> GetAuthorsByGenreAsync(string genre)
    {
        return await _context.Boeken
            .Where(x => x.Genre.ToLower() == genre.ToLower())
            .Select(x => x.Auteur)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }

    public async Task<double> GetAveragePriceAsync()
    {
        var count = await _context.Boeken.CountAsync();
        if (count == 0)
            return 0;

        return await _context.Boeken.AverageAsync(x => x.Prijs);
    }

    public async Task<double> GetMinPriceAsync()
    {
        var count = await _context.Boeken.CountAsync();
        if (count == 0)
            return 0;

        return await _context.Boeken.MinAsync(x => x.Prijs);
    }

    public async Task<double> GetMaxPriceAsync()
    {
        var count = await _context.Boeken.CountAsync();
        if (count == 0)
            return 0;

        return await _context.Boeken.MaxAsync(x => x.Prijs);
    }

    public async Task<List<Boek>> GetBooksByAuthorAsync(string auteur)
    {
        return await _context.Boeken
            .Where(x => x.Auteur.ToLower() == auteur.ToLower())
            .OrderBy(x => x.Prijs)
            .ToListAsync();
    }

    public async Task<List<Boek>> GetBooksWithVoorraadGreaterThanAsync(int minVoorraad)
    {
        return await _context.Boeken
            .Where(x => x.Voorraad > minVoorraad)
            .OrderBy(x => x.Prijs)
            .ToListAsync();
    }

    public async Task<bool> HasAnyBookInGenreAsync(string genre)
    {
        return await _context.Boeken
            .AnyAsync(x => x.Genre.ToLower() == genre.ToLower());
    }

    public async Task<List<string>> GetPublisherNamesAsync()
    {
        return await _context.Boeken
            .Select(x => x.Uitgeverij)
            .Distinct()
            .OrderBy(x => x)
            .ToListAsync();
    }
}
