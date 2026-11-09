using ProgrammingAdvancedOefeningen.Repositories;

namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentilatorController : ControllerBase
    {
        private readonly IVentilatorRepository _ventilatorRepository;
        private readonly ILogger<VentilatorController> _logger;

        public VentilatorController(IVentilatorRepository ventilatorRepository, ILogger<VentilatorController> logger)
        {
            _ventilatorRepository = ventilatorRepository;
            _logger = logger;
        }

        [HttpGet()]
        public ActionResult<List<Ventilator>> GetVentilatoren()
        {
            _logger.LogInformation("GET request ontvangen voor alle ventilatoren");
            return Ok(_ventilatorRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Ventilator> GetVentilatorById(int id)
        {
            _logger.LogInformation($"GET request received for id: {id}");
            Ventilator? ventilator = _ventilatorRepository.GetVentilatorById(id);

            if (ventilator == null)
            {
                _logger.LogWarning($"Ventilator met ID: {id} niet gevonden!");
                return NotFound($"We vonden geen ventilator met Id {id}.");
            }

            return Ok(ventilator);
        }

        [HttpPost]
        public ActionResult<Ventilator> CreateVentilator(Ventilator nieuweVentilator)
        {
            if (string.IsNullOrWhiteSpace(nieuweVentilator.Merk))
            {
                _logger.LogWarning($"Het veld merk mag niet leeg zijn.");
                return BadRequest("Een ventilator moet een merk hebben.");
            }
            
            if (nieuweVentilator.AfmetingInMm != 120 && nieuweVentilator.AfmetingInMm != 140)
            {
                _logger.LogWarning($"Het veld afmeting mag niet leeg, kleiner dan 120 of groter dan 140 zijn.");
                return BadRequest("We verkopen enkel ventilatoren van 120 of 140 mm.");
            }
            
            if (nieuweVentilator.Prijs <= 0)
            {
                _logger.LogWarning($"Het veld prijs mag niet negatief of onder nul zijn.");
                return BadRequest("De prijs moet groter zijn dan 0.");
            }

            Ventilator gemaakteVentilator = _ventilatorRepository.Create(nieuweVentilator);

            return CreatedAtAction(nameof(GetVentilatorById), new { id = gemaakteVentilator.Id }, gemaakteVentilator);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateVentilator(int id, Ventilator bijgewerkteVentilator)
        {
            if (_ventilatorRepository.GetVentilatorById(id) == null)
            {
                return NotFound($"Kan geen ventilator bijwerken met Id {id}, omdat deze niet bestaat.");
            }

            if (string.IsNullOrWhiteSpace(bijgewerkteVentilator.Merk))
            {
                return BadRequest("Een ventilator moet een merk hebben.");
            }
            
            if (bijgewerkteVentilator.AfmetingInMm != 120 && bijgewerkteVentilator.AfmetingInMm != 140)
            {
                return BadRequest("We verkopen enkel ventilatoren van 120 of 140 mm.");
            }
            
            if (bijgewerkteVentilator.Prijs <= 0)
            {
                return BadRequest("De prijs moet groter zijn dan 0.");
            }

            _ventilatorRepository.Update(id, bijgewerkteVentilator);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVentilator(int id)
        {
            Ventilator? ventilator = _ventilatorRepository.GetVentilatorById(id);

            if (ventilator == null)
            {
                return NotFound($"Kan ventilator met Id {id} niet verwijderen, omdat deze niet gevonden is.");
            }

            _ventilatorRepository.Delete(id);

            return NoContent();
        }
    }
}
