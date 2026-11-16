namespace ExamenMuziekschool.Repositories
{
    public interface ILesgeverRepository
    {
        Task<List<Lesgever>> GetAllLesgeversAsync(); // HttpGet element
        Task<Lesgever> CreateLesgeverAsync(Lesgever lesgever); // HttpPost element
        Task<Lesgever?> GetLesgeverByIdAsync(int id); // HttpGet element
        Task WijzigLesgeverAsync(int id, Lesgever lesgever); // HttpPut element
        Task DeleteLesgeverAsync(int id); // HttpDelete element
    }
}
