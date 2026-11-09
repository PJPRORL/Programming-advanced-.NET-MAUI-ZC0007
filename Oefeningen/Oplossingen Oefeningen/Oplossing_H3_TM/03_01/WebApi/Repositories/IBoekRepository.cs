using WebApi.Models;

namespace WebApi.Repositories;

public interface IBoekRepository
{
    List<Boek> GetAll();
    Boek? GetById(int id);
    Boek Create(Boek boek);
}
