namespace ExamenMuziekschool.Repositories
{
    public interface ILeerlingRepository
    {
        Task<List<Leerling>> GetAllLeerlingenAsync(); // HttpGet element
        Task<Leerling> CreateLeerlingAsync(Leerling leerling); // HttpPost element
        Task<Leerling?> GetLeerlingByIdAsync(int id); // HttpGet element
        Task<List<Leerling>> GetLeerlingByNaamAsync(string naam); // HttpGet element
        Task WijzigLeerlingAsync(int id, Leerling leerling); // HttpPut element
        Task DeleteLeerlingAsync(int id); // HttpDelete element
    }
}
