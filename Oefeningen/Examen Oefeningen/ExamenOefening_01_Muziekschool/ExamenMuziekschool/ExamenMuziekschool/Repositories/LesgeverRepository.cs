namespace ExamenMuziekschool.Repositories
{
    public class LesgeverRepository : ILesgeverRepository
    {
        private readonly MuziekschoolContextDB _context;

        public LesgeverRepository(MuziekschoolContextDB context)
        {
            _context = context;
        }

        public async Task<List<Lesgever>> GetAllLesgeversAsync() // HttpGet element
        {
            return await _context.Lesgevers
                .OrderBy(lesgever => lesgever.Naam)
                .ToListAsync();
        }
        public async Task<Lesgever> CreateLesgeverAsync(Lesgever nieuweLesgever) // HttpPost element
        {
            int hoogsteId = 0;

            if (await _context.Lesgevers.AnyAsync())
            {
                hoogsteId = await _context.Lesgevers.MaxAsync(lesgever => lesgever.Id);
            }

            nieuweLesgever.Id = hoogsteId + 1;

            _context.Lesgevers.Add(nieuweLesgever);
            await _context.SaveChangesAsync();

            return nieuweLesgever;
        }
        public async Task<Lesgever?> GetLesgeverByIdAsync(int id) // HttpGet element
        {
            return await _context.Lesgevers.FindAsync(id);
        }
        public async Task WijzigLesgeverAsync(int id, Lesgever lesgever) // HttpPut element
        {
            Lesgever? bestaandeLesgever = await _context.Lesgevers.FindAsync(id);

            if (bestaandeLesgever != null)
            {
                bestaandeLesgever.Naam = lesgever.Naam;
                bestaandeLesgever.Voornaam = lesgever.Voornaam;
                bestaandeLesgever.Email = lesgever.Email;
                bestaandeLesgever.LesgeverInstrumenten = lesgever.LesgeverInstrumenten;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteLesgeverAsync(int id) // HttpDelete element
        {
            Lesgever? Lesgever = await _context.Lesgevers.FindAsync(id);

            if (Lesgever != null)
            {
                _context.Lesgevers.Remove(Lesgever);
                await _context.SaveChangesAsync();
            }
        }
    }
}
