using WebApi.Models;

namespace WebApi.Repositories;

public interface IBestellingRepository
{
    // CRUD-methodes
    Task<Bestelling> CreateAsync(Bestelling bestelling);
    Task DeleteAsync(int id);
    Task<List<Bestelling>> GetAllAsync();
    Task<Bestelling?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Bestelling bestelling);

    // Query-methodes
    Task<List<Bestelling>> GetByStatusAsync(string status);
    Task<List<Bestelling>> GetByTafelAsync(int tafelnnummer);
    Task<List<Bestelling>> GetSortedByTotaalPrijsAsync(bool descending);
    Task<double> GetTotalRevenueAsync();
    Task<double> GetAverageOrderValueAsync();
    Task<int> CountByStatusAsync(string status);
    Task<bool> HasAnyOrderWithStatusAsync(string status);
}
