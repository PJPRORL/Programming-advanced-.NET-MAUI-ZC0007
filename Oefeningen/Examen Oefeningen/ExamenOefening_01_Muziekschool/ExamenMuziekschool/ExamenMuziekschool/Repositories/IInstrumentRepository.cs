namespace ExamenMuziekschool.Repositories
{
    public interface IInstrumentRepository
    {
        Task<List<Instrument>> GetAllInstrumentenAsync(); // HttpGet element
        Task<Instrument> CreateInstrumentAsync(Instrument instrument); // HttpPost element
        Task<Instrument?> GetInstrumentByIdAsync(int id); // HttpGet element
        Task WijzigInstrumentAsync(int id, Instrument instrument); // HttpPut element
        Task DeleteInstrumentAsync(int id); // HttpDelete element
    }
}
