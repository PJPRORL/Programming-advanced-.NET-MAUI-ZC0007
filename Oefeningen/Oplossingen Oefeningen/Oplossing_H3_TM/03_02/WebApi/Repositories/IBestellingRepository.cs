using WebApi.Models;

namespace WebApi.Repositories;

public interface IBestellingRepository
{
    List<Bestelling> GetAll();
    Bestelling? GetById(int id);
    Bestelling Create(Bestelling bestelling);
    void Update(int id, Bestelling bestelling);
    void Delete(int id);
}
