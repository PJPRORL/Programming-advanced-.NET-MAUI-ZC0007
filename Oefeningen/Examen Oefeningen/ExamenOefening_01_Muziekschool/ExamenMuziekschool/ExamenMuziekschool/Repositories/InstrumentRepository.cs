namespace ExamenMuziekschool.Repositories
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly MuziekschoolContextDB _context;

        public InstrumentRepository(MuziekschoolContextDB context)
        {
            _context = context;
        }

        public async Task<List<Instrument>> GetAllInstrumentenAsync()
        {
            return await _context.Instrumenten
                .OrderBy(instrument => instrument.Naam)
                .ToListAsync();
        }
        public async Task<Instrument> CreateInstrumentAsync(Instrument nieuwInstrument)
        {
            int hoogsteId = 0;

            if (await _context.Instrumenten.AnyAsync())
            {
                hoogsteId = await _context.Instrumenten.MaxAsync(instrument => instrument.Id);
            }

            nieuwInstrument.Id = hoogsteId + 1;

            _context.Instrumenten.Add(nieuwInstrument);
            await _context.SaveChangesAsync();

            return nieuwInstrument;
        }
        public async Task<Instrument?> GetInstrumentByIdAsync(int id)
        {
            return await _context.Instrumenten.FindAsync(id);
        }
        public async Task WijzigInstrumentAsync(int id, Instrument instrument)
        {
            Instrument? bestaandInstrument = await _context.Instrumenten.FindAsync(id);

            if (bestaandInstrument != null)
            {
                bestaandInstrument.Naam = instrument.Naam;
                bestaandInstrument.Categorie = instrument.Categorie;
                bestaandInstrument.Leerlingen = instrument.Leerlingen;
                bestaandInstrument.LesgeverInstrumenten = instrument.LesgeverInstrumenten;
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteInstrumentAsync(int id)
        {
            Instrument? instrument = await _context.Instrumenten.FindAsync(id);

            if (instrument != null)
            {
                _context.Instrumenten.Remove(instrument);
                await _context.SaveChangesAsync();
            }
        }
    }
}
