namespace ExamenNovember.Repositories
{
    public interface IDocentRepository
    {
        Task<List<Docent>> GetAllDocentenAsync();
        Task<Docent> GetDocentByIdAsync(int id);
        Task<Docent> CreateDocentAsync(Docent docent);
        Task UpdateDocentAsync(int id, Docent docent);
        Task DeleteDocentAsync(int id);
    }
}
