using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingAdvancedOefeningen.Models;
using System.Security.Cryptography.X509Certificates;

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

        // Verwerken van artikelen
        public ActionResult<List<Artikel>> GetArtikelen()
        {
            return Ok(Artikelen);
        }

        [HttpGet("artikel/{id}")]
        public ActionResult<Artikel> GetArtikelId(int id)
        {
            Artikel artikel = Artikelen.FirstOrDefault(artikel => artikel.Id == id);

            return artikel;
        }
    }
}
