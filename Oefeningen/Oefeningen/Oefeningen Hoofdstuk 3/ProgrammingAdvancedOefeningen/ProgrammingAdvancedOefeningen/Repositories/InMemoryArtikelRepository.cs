using ProgrammingAdvancedOefeningen.Controllers;

namespace ProgrammingAdvancedOefeningen.Repositories
{
    public class InMemoryArtikelRepository : IArtikelRepository
    {
        

        // Lijst met artikelen maken
        private static List<Artikel> artikelen = new List<Artikel>
        {
            new Artikel { Id = 1, Naam = "Draadloze Muis", Merk = "Logitech", Soort = "Elektronica", Prijs = 29.99, AantalOpVoorraad = 45 },
            new Artikel { Id = 2, Naam = "Mechanisch Toetsenbord", Merk = "Keychron", Soort = "Elektronica", Prijs = 89.50, AantalOpVoorraad = 18 },
            new Artikel { Id = 3, Naam = "Noise-Cancelling Koptelefoon", Merk = "Sony", Soort = "Audio", Prijs = 199.95, AantalOpVoorraad = 12 },
            new Artikel { Id = 4, Naam = "Monitor 27 inch", Merk = "Dell", Soort = "Schermen", Prijs = 249.00, AantalOpVoorraad = 0 },
            new Artikel { Id = 5, Naam = "USB-C Docking Station", Merk = "Anker", Soort = "Accessoires", Prijs = 59.99, AantalOpVoorraad = 30 },
            new Artikel { Id = 6, Naam = "Ergonomische Bureaustoel", Merk = "Herman Miller", Soort = "Kantoormeubilair", Prijs = 495.00, AantalOpVoorraad = 0 }
        };

        public List<Artikel> GetAll()
        {
            return artikelen;
        }

        public Artikel? GetArtikelById(int id)
        {
            return artikelen.FirstOrDefault(item => item.Id == id);
        }

        public List<Artikel> GetArtikelBySoort(string soort)
        {
            return artikelen
                .Where(artikel => artikel.Soort.ToLower() == soort.ToLower())
                .ToList();
        }

        public List<Artikel> GetVoorraadArtikel()
        {
            return artikelen
                .Where(artikel => artikel.AantalOpVoorraad >= 0)
                .ToList();
        }

        public List<Artikel> GetArtikelMerk(string merk)
        {
            return artikelen
                .Where(artikel => artikel.Merk.ToLower() == merk.ToLower())
                .ToList();
        }

        public Artikel Create(Artikel nieuwArtikel)
        {
            int nieuweId = artikelen.Max(item => item.Id) + 1;
            nieuwArtikel.Id = nieuweId;

            artikelen.Add(nieuwArtikel);
            return nieuwArtikel;
        }

        public void Update(int id, Artikel bijgewerktArtikel)
        {
            Artikel? bestaandArtikel = artikelen.FirstOrDefault(item => item.Id == id);

            if (bestaandArtikel != null)
            {
                bestaandArtikel.Merk = bijgewerktArtikel.Merk;
                bestaandArtikel.Naam = bijgewerktArtikel.Naam;
                bestaandArtikel.Prijs = bijgewerktArtikel.Prijs;
                bestaandArtikel.Soort = bijgewerktArtikel.Soort;
            }
        }

        public void Delete(int id)
        {
            Artikel? artikel = artikelen.FirstOrDefault(item => item.Id == id);

            if (artikel != null)
            {
                artikelen.Remove(artikel);
            }
        }
    }
}
