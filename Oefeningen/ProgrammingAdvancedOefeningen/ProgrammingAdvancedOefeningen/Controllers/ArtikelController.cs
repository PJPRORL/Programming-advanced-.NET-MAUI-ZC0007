namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikelController : ControllerBase
    {
        // Lijst met artikelen maken
        List<Artikel> Artikelen = new List<Artikel>
        {
            new Artikel { Id = 1, Naam = "Draadloze Muis", Merk = "Logitech", Soort = "Elektronica", Prijs = 29.99, AantalOpVoorraad = 45 },
            new Artikel { Id = 2, Naam = "Mechanisch Toetsenbord", Merk = "Keychron", Soort = "Elektronica", Prijs = 89.50, AantalOpVoorraad = 18 },
            new Artikel { Id = 3, Naam = "Noise-Cancelling Koptelefoon", Merk = "Sony", Soort = "Audio", Prijs = 199.95, AantalOpVoorraad = 12 },
            new Artikel { Id = 4, Naam = "Monitor 27 inch", Merk = "Dell", Soort = "Schermen", Prijs = 249.00, AantalOpVoorraad = 0 },
            new Artikel { Id = 5, Naam = "USB-C Docking Station", Merk = "Anker", Soort = "Accessoires", Prijs = 59.99, AantalOpVoorraad = 30 },
            new Artikel { Id = 6, Naam = "Ergonomische Bureaustoel", Merk = "Herman Miller", Soort = "Kantoormeubilair", Prijs = 495.00, AantalOpVoorraad = 0 }
        };


        [HttpGet()]
        public ActionResult<List<Artikel>> GetArtikelen()
        {
            return Ok(Artikelen);
        }

        [HttpGet("{id}")]
        public ActionResult<Artikel> GetArtikelId(int id)
        {

            Artikel artikel = Artikelen.FirstOrDefault(artikel => artikel.Id == id);

            if (artikel == null)
            {
                return NotFound($"We vonden geen artikel met Id {id}.");
            }

            return Ok(artikel);
        }

        [HttpGet("soort/{soort}")]
        public ActionResult<Artikel> GetArtikelSoort(string soort)
        {

            List<Artikel> artikelen = new List<Artikel>();

            foreach (var artikel in Artikelen)
            {
                if (artikel.Soort == soort)
                {
                    artikelen.Add(artikel);
                }
            }

            if (artikelen.Count == 0)
            {
                return NotFound($"We vonden geen artikelen van de soort {soort}.");
            }
            else
            {
                return Ok(artikelen);
            }
        }

        [HttpGet("voorradig")]
        public ActionResult<Artikel> GetVoorraad()
        {
            List<Artikel> artikelen = new List<Artikel>();

            foreach (var artikel in Artikelen)
            {
                if (artikel.AantalOpVoorraad > 0)
                {
                    artikelen.Add(artikel);
                }
            }

            if (artikelen.Count == null)
            {
                return Ok(artikelen);
            }

            return Ok(artikelen);
        }

        //[HttpGet("fiche/{id}")]
        //public ActionResult<ArtikelFiche> GetFicheArtikelen()
        //{


        //    return ;
        //}
    }
}
