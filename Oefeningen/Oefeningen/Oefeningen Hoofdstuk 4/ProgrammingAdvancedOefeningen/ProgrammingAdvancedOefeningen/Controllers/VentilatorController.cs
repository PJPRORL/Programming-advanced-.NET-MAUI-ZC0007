using ProgrammingAdvancedOefeningen.Repositories;

namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentilatorController : ControllerBase
    {
        private readonly IVentilatorRepository _ventilatorRepository;
        private readonly IWijzigingRepository _wijzigingRepository;
        private readonly ILogger<VentilatorController> _logger;

        public VentilatorController(IVentilatorRepository ventilatorRepository, IWijzigingRepository wijzigingRepository, ILogger<VentilatorController> logger)
        {
            _ventilatorRepository = ventilatorRepository;
            _wijzigingRepository = wijzigingRepository;
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
            _logger.LogInformation($"Create request received for a new ventilator");

            if (string.IsNullOrWhiteSpace(nieuweVentilator.Merk))
            {
                _logger.LogWarning("Het veld merk mag niet leeg zijn.");
                return BadRequest("Een ventilator moet een merk hebben.");
            }
            
            if (nieuweVentilator.AfmetingInMm != 120 && nieuweVentilator.AfmetingInMm != 140)
            {
                _logger.LogWarning("Het veld mag enkel een afmeting hebben van 120 of 140.");
                return BadRequest("We verkopen enkel ventilatoren van 120 of 140 mm.");
            }
            
            if (nieuweVentilator.Prijs <= 0)
            {
                _logger.LogWarning("Het veld prijs moet boven 0 zijn.");
                return BadRequest("De prijs moet groter zijn dan 0.");
            }

            Ventilator gemaakteVentilator = _ventilatorRepository.Create(nieuweVentilator);

            return CreatedAtAction(nameof(GetVentilatorById), new { id = gemaakteVentilator.Id }, gemaakteVentilator);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateVentilator(int id, Ventilator bijgewerkteVentilator)
        {
            _logger.LogInformation($"PUT request received for ventilator with ID: {id}.");

            if (_ventilatorRepository.GetVentilatorById(id) == null)
            {
                _logger.LogWarning($"Kan geen ventilator met ID: {id} terugvinden.");
                return NotFound($"Kan geen ventilator bijwerken met Id {id}, omdat deze niet bestaat.");
            }

            if (string.IsNullOrWhiteSpace(bijgewerkteVentilator.Merk))
            {
                _logger.LogWarning($"Het veld merk mag niet leeg zijn.");
                return BadRequest("Een ventilator moet een merk hebben.");
            }
            
            if (bijgewerkteVentilator.AfmetingInMm != 120 && bijgewerkteVentilator.AfmetingInMm != 140)
            {
                _logger.LogWarning("Een ventilator met een afmeting van 120 of 140 mm is enkel toegestaan.");
                return BadRequest("We verkopen enkel ventilatoren van 120 of 140 mm.");
            }
            
            if (bijgewerkteVentilator.Prijs <= 0)
            {
                _logger.LogWarning("De prijs mag niet onder 0 of 0 zijn.");
                return BadRequest("De prijs moet groter zijn dan 0.");
            }

            _ventilatorRepository.Update(id, bijgewerkteVentilator);
            _logger.LogInformation($"PUT request received and ventilator met ID: {id} aangepast");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteVentilator(int id)
        {
            _logger.LogInformation($"Delete request received for ventilator with ID: {id}.");
            Ventilator? ventilator = _ventilatorRepository.GetVentilatorById(id);

            if (ventilator == null)
            {
                _logger.LogWarning($"Kan geen ventilator met ID: {id} terugvinden. De ventilator kan niet worden verwijderd.");
                return NotFound($"Kan ventilator met Id {id} niet verwijderen, omdat deze niet gevonden is.");
            }

            _ventilatorRepository.Delete(id);
            _logger.LogInformation($"Delete request received and ventilator with ID: {id} deleted.");
            _wijzigingRepository.GeslaagdeWijziging("verwijderd", id);

            return NoContent();
        }

        [HttpGet("historiek")]
        public ActionResult<Wijziging> GetAllWijzigingen()
        {
            return Ok(_wijzigingRepository);
        }
    }
}
