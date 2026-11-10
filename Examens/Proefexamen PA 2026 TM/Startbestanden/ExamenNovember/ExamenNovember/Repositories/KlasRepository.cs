namespace ExamenNovember.Repositories
{
    public class KlasRepository : IKlasRepository
    {
        private readonly ExamenNovemberContext _context;

        public KlasRepository(ExamenNovemberContext context)
        {
            _context = context;
        }

        public async Task<Klas> CreateKlasAsync(Klas klas)
        {
            _context.Klassen.Add(klas);
            _context.SaveChanges();
            return klas;
        }

        public async Task DeleteKlasAsync(int id)
        {
            Klas? klas = _context.Klassen.Find(id);

            if (klas != null)
            {
                _context.Klassen.Remove(klas);
                _context.SaveChanges();
            }
        }

        public async Task<List<Klas>> GetAllKlassenAsync()
        {
            return _context.Klassen.OrderBy(docent => docent.Naam).ToList();
        }

        public async Task<Klas> GetKlasByIdAsync(int id)
        {
            return _context.Klassen.Find(id);
        }

        public async Task UpdateKlasAsync(int id, Klas klas)
        {
            Klas? bestaandeKlas = _context.Klassen.Find(id);

            if (bestaandeKlas != null)
            {
                bestaandeKlas.Naam = klas.Naam;
                bestaandeKlas.Schooljaar = klas.Schooljaar;
                _context.SaveChanges();
            }
        }
    }
}
