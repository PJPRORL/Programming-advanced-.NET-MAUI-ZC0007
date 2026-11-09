using WebApi.Models;

namespace WebApi.Repositories;

public interface IFietsRepository
{
    Task<Fiets> CreateAsync(Fiets fiets);
    Task DeleteAsync(int id);
    Task<List<Fiets>> GetAllAsync();
    Task<Fiets?> GetByIdAsync(int id);
    Task UpdateAsync(int id, Fiets fiets);
    Task<List<Fiets>> GetByMerkAsync(string merk);
    Task<List<Fiets>> GetByTypeAsync(string type);
    Task<List<Fiets>> GetByKleurAsync(string kleur);
    Task<List<Fiets>> GetInStockAsync();
    Task<List<Fiets>> GetSortedByPrijsAsync(bool descending);
    Task<bool> ExistsByMerkAsync(string merk);
    Task<int> CountByTypeAsync(string type);
}
