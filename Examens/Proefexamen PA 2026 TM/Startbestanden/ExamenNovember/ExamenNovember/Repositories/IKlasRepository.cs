namespace ExamenNovember.Repositories
{
    public interface IKlasRepository
    {
        Task<List<Klas>> GetAllKlassenAsync();
        Task<Klas> GetKlasByIdAsync(int id);
        Task<Klas> CreateKlasAsync(Klas klas);
        Task UpdateKlasAsync(int id, Klas klas);
        Task DeleteKlasAsync(int id);
    }
}
