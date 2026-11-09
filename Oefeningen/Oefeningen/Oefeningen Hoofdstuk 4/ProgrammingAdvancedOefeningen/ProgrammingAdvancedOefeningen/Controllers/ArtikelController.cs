using ProgrammingAdvancedOefeningen.Repositories;

namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtikelController : ControllerBase
    {
        private readonly IArtikelRepository _ArtikelRepository;
        private readonly ILogger<ArtikelController> _Logger;

        // Constructor
        public ArtikelController(IArtikelRepository artikelRepository, ILogger<ArtikelController> logger)
        {
            _ArtikelRepository = artikelRepository;
            _Logger = logger;
        }

        [HttpGet()]
        public ActionResult<List<Artikel>> GetArtikelen()
        {
            _Logger.LogInformation("GET request ontvangen voor alle artikelen");
            return Ok(_ArtikelRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Artikel> GetArtikelId(int id)
        {
            _Logger.LogInformation($"GET request voor artikel met ID: {id}");
            Artikel? artikel = _ArtikelRepository.GetArtikelById(id);

            if (artikel == null)
            {
                _Logger.LogWarning($"Artikel met ID {id} werd niet gevonden");
                return NotFound($"We vonden geen artikel met Id {id}.");
            }

            return Ok(artikel);
        }

        [HttpGet("soort/{soort}")]
        public ActionResult<List<Artikel>> GetArtikelSoort(string soort)
        {

            _Logger.LogInformation($"GET request ontvangen voor artikelen met soort: {soort}");
            List<Artikel>? artikelen = _ArtikelRepository.GetArtikelBySoort(soort);

            if (artikelen == null || artikelen.Count == 0)
            {
                _Logger.LogWarning($"Geen artikelen gevonden met soort: {soort}");
                return NotFound($"We vonden geen artikelen van de soort {soort}.");
            }

            return Ok(artikelen);
        }

        [HttpGet("merk/{merk}")]
        public ActionResult<List<Artikel>> GetArtikelMerk(string merk)
        {
            _Logger.LogInformation($"GET request ontvangen voor artikelen met merk: {merk}");
            List<Artikel>? artikels = _ArtikelRepository.GetArtikelMerk(merk);

            if (artikels == null || artikels.Count == 0)
            {
                _Logger.LogWarning($"We vonden geen artikelen van het merk {merk}.");
                return NotFound($"Geen artikelen gevonden van het merk: {merk}");
            }

            return Ok(artikels);
        }

        [HttpGet("voorradig")]
        public ActionResult<List<Artikel>> GetVoorraad()
        {
            List<Artikel> artikelVoorraad = _ArtikelRepository.GetVoorraadArtikel();

            return Ok(artikelVoorraad);
        }

        //[HttpGet("fiche/{id}")]
    }
}
