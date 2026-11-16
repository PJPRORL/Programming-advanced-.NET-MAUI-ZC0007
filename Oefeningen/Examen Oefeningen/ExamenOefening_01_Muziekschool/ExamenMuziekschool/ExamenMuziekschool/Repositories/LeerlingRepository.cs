namespace ExamenMuziekschool.Repositories
{
    public class LeerlingRepository : ILeerlingRepository
    {
        private readonly MuziekschoolContextDB _context;

        public LeerlingRepository(MuziekschoolContextDB context)
        {
            _context = context;
        }

        public async Task<List<Leerling>> GetAllLeerlingenAsync()
        {
            return await _context.Leerlingen
                .OrderBy(leerling => leerling.Naam)
                .ToListAsync();
        }
        public async Task<Leerling> CreateLeerlingAsync(Leerling nieuweLeerling)
        {
            int hoogsteId = 0;

            if (await _context.Leerlingen.AnyAsync())
            {
                hoogsteId = await _context.Leerlingen.MaxAsync(leerling => leerling.Id);
            }

            nieuweLeerling.Id = hoogsteId + 1;

            _context.Leerlingen.Add(nieuweLeerling);
            await _context.SaveChangesAsync();

            return nieuweLeerling;
        }
        public async Task<Leerling?> GetLeerlingByIdAsync(int id)
        {
            return await _context.Leerlingen.FindAsync(id);
        }
        public async Task<List<Leerling>> GetLeerlingByNaamAsync(string naam)
        {
            return await _context.Leerlingen
                .Where(leerling => leerling.Naam.ToLower() == naam.ToLower())
                .OrderBy(leerling => leerling.Voornaam)
                .ToListAsync();
        }
        public async Task WijzigLeerlingAsync(int id, Leerling leerling)
        {
            Leerling? bestaandeLeerling = await _context.Leerlingen.FindAsync(id);

            if (bestaandeLeerling != null)
            {
                bestaandeLeerling.Naam = leerling.Naam;
                bestaandeLeerling.Voornaam = leerling.Voornaam;
                bestaandeLeerling.Email = leerling.Email;
                bestaandeLeerling.Inschrijfdatum = leerling.Inschrijfdatum;
                bestaandeLeerling.Instrument = leerling.Instrument;
                bestaandeLeerling.InstrumentId = leerling.InstrumentId;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteLeerlingAsync(int id)
        {
            Leerling? leerling = await _context.Leerlingen.FindAsync(id);

            if (leerling != null)
            {
                _context.Leerlingen.Remove(leerling);
                await _context.SaveChangesAsync();
            }
        }
    }
}
