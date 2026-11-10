namespace ExamenNovember.Repositories
{
    public class DocentRepository : IDocentRepository
    {
        private readonly ExamenNovemberContext _context;

        public DocentRepository(ExamenNovemberContext context)
        {
            _context = context;
        }

        public async Task<List<Docent>> GetAllDocentenAsync()
        {
            return _context.Docenten.OrderBy(docent => docent.Naam).ToList();
        }

        public async Task<Docent> GetDocentByIdAsync(int id)
        {
            return _context.Docenten.Find(id);
        }

        public async Task<Docent> CreateDocentAsync(Docent docent)
        {
            _context.Docenten.Add(docent);
            _context.SaveChanges();
            return docent;
        }

        public async Task DeleteDocentAsync(int id)
        {
            Docent? docent = _context.Docenten.Find(id);

            if (docent != null)
            {
                _context.Docenten.Remove(docent);
                _context.SaveChanges();
            }
        }

        public async Task UpdateDocentAsync(int id, Docent docent)
        {
            Docent? bestaandeDocent = _context.Docenten.Find(id);

            if (bestaandeDocent != null)
            {
                bestaandeDocent.Naam = docent.Naam;
                bestaandeDocent.Voornaam = docent.Voornaam;
                _context.SaveChanges();
            }
        }
    }
}
