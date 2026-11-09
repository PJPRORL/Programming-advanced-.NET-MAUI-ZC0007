namespace ProgrammingAdvancedTheorie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILaptopRepository _laptopRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILaptopRepository laptopRepository, ILogger<ProductController> logger)
        {
            _laptopRepository = laptopRepository;
            _logger = logger;
        }

        // 1. GET: Alle laptops ophalen
        // Route: GET api/laptops
        [HttpGet]
        public ActionResult<List<Laptop>> GetAlleLaptops()
        {
            // We sturen een 200 OK status terug samen met de lijst
            _logger.LogInformation("GET request ontvangen voor alle laptops.");
            return Ok(_laptopRepository.GetAll());
        }

        // 2. GET: Eén specifieke laptop op basis van Id
        // Route: GET api/laptops/1
        [HttpGet("{id}")]
        public ActionResult<Laptop> GetLaptopById(int id)
        {
            _logger.LogInformation($"GET request voor laptop met ID: {id}");
            Laptop laptop = _laptopRepository.GetById(id);

            // Als de laptop niet bestaat, stuur dan een 404 terug
            if (laptop == null)
            {
                _logger.LogWarning($"Laptop met ID {id} werd niet gevonden.");
                return NotFound($"Helaas, we konden geen laptop vinden met Id {id}.");
            }

            return Ok(laptop);
        }

        [HttpGet("merk/{merk}")]
        public ActionResult<List<Laptop>> GetLaptopsByMerk(string merk)
        {
            List<Laptop>? laptops = _laptopRepository.GetLaptopsByMerk(merk);

            if (laptops == null || laptops.Count == 0)
            {
                _logger.LogWarning($"Geen laptops gevonden met merk: {merk}");
                return NotFound($"Helaas, we konden geen laptops vinden met merk {merk}.");
            }

            return Ok(laptops);
        }

        // 3. POST: Een nieuwe laptop toevoegen
        // Route: POST api/laptops
        [HttpPost]
        public ActionResult<Laptop> CreateLaptop(Laptop nieuweLaptop)
        {
            _logger.LogInformation($"Aanmaken van nieuwe laptop: {nieuweLaptop.Merk}");
            Laptop aangemaakteLaptop = _laptopRepository.Create(nieuweLaptop);

            // We retourneren een 201 Created statuscode.
            // Dit vertelt de client dat het object succesvol is aangemaakt én waar het te vinden is.
            _logger.LogInformation($"Laptop succesvol aangemaakt met ID: {aangemaakteLaptop.Id}");
            return CreatedAtAction(nameof(GetLaptopById), new { id = aangemaakteLaptop.Id }, aangemaakteLaptop);
        }

        // 4. PUT: Een bestaande laptop volledig bijwerken
        // Route: PUT api/laptops/1
        [HttpPut("{id}")]
        public ActionResult UpdateLaptop(int id, Laptop bijgewerkteLaptop)
        {
            _logger.LogInformation($"Update request voor laptop met ID: {id}");
            Laptop bestaandeLaptop = _laptopRepository.GetById(id);

            // 2. Als de laptop niet bestaat, sturen we een 404 Not Found
            if (bestaandeLaptop == null)
            {
                _logger.LogWarning($"Update mislukt: Laptop met ID {id} bestaat niet.");
                return NotFound($"Kan geen laptop bijwerken met Id {id}, omdat deze niet bestaat.");
            }

            _laptopRepository.Update(id, bijgewerkteLaptop);

            _logger.LogInformation($"Laptop met ID {id} is succesvol bijgewerkt.");
            // 4. Update succesvol! We sturen een 204 No Content terug.
            return NoContent();
        }


        // 5. DELETE: Een laptop verwijderen
        // Route: DELETE api/laptops/1
        [HttpDelete("{id}")]
        public ActionResult DeleteLaptop(int id)
        {
            _logger.LogInformation($"Delete request voor laptop met ID: {id}");
            Laptop laptop = _laptopRepository.GetById(id);

            // 2. Bestaat hij niet Dan kunnen we hem ook niet verwijderen -> 404!
            if (laptop == null)
            {
                _logger.LogError($"Delete mislukt: Laptop met ID {id} werd niet gevonden.");
                return NotFound($"Kan laptop met Id {id} niet verwijderen, omdat deze niet is gevonden.");
            }

            _laptopRepository.Delete(id);

            _logger.LogInformation($"Laptop met ID {id} is succesvol verwijderd.");
            // 4. Succesvol verwijderd -> We sturen een 204 No Content terug
            return NoContent();
        }
    }
}
