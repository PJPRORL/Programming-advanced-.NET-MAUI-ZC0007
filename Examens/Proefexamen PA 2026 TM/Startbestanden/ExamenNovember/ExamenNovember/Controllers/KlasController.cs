namespace ExamenNovember.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KlasController : ControllerBase
    {
        private readonly IKlasRepository _repository;

        public KlasController(IKlasRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Klas>>> GetAllKlassen()
        {
            return Ok(await _repository.GetAllKlassenAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Klas>> GetKlasById(int id)
        {
            Klas? klas = await _repository.GetKlasByIdAsync(id);

            if (klas == null)
            {
                return NotFound($"We vonden geen klas met Id: {id}");
            }

            return Ok(klas);
        }

        [HttpPost()]
        public async Task<ActionResult<Klas>> CreateKlas(Klas nieuweKlas)
        {
            Klas gemaakteKlas = await _repository.CreateKlasAsync(nieuweKlas);

            return CreatedAtAction(nameof(GetKlasById), new { id = gemaakteKlas.Id }, gemaakteKlas);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteKlas(int id)
        {
            if (await _repository.GetKlasByIdAsync(id) == null)
            {
                return NotFound($"We vonden geen klas met Id {id}");
            }

            await _repository.DeleteKlasAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateKlas(int id, Klas gewijzigdKlas)
        {
            if (await _repository.GetKlasByIdAsync(id) == null)
            {
                return NotFound($"We vonden geen klas met Id {id}");
            }

            await _repository.UpdateKlasAsync(id, gewijzigdKlas);

            return NoContent();
        }
    }
}
