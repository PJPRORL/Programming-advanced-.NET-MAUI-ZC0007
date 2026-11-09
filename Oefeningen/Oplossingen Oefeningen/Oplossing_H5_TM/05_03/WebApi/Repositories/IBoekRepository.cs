using WebApi.Models;

namespace WebApi.Repositories;

public interface IBoekRepository
{
    // CRUD-methodes
    Task<Boek> CreateAsync(Boek boek);
    Task DeleteAsync(int id);
    Task<List<Boek>> GetAllAsync();
    Task<Boek?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Boek boek);

    // Query-methodes
    Task<List<string>> GetUniqueGenresAsync();
    Task<List<string>> GetAuthorsByGenreAsync(string genre);
    Task<double> GetAveragePriceAsync();
    Task<double> GetMinPriceAsync();
    Task<double> GetMaxPriceAsync();
    Task<List<Boek>> GetBooksByAuthorAsync(string auteur);
    Task<List<Boek>> GetBooksWithVoorraadGreaterThanAsync(int minVoorraad);
    Task<bool> HasAnyBookInGenreAsync(string genre);
    Task<List<string>> GetPublisherNamesAsync();
}
