namespace ExamenNovember.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentController : ControllerBase
    {
        private readonly IDocentRepository _repository;

        public DocentController(IDocentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Docent>>> GetAllDocenten()
        {
            return Ok(await _repository.GetAllDocentenAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Docent>> GetDocentById(int id)
        {
            Docent? docent = await _repository.GetDocentByIdAsync(id);

            if (docent == null)
            {
                return NotFound($"We vonden geen docent met Id: {id}");
            }

            return Ok(docent);
        }

        [HttpPost()]
        public async Task<ActionResult<Docent>> CreateDocent(Docent nieuweDocent)
        {
            Docent gemaakteDocent = await _repository.CreateDocentAsync(nieuweDocent);

            return CreatedAtAction(nameof(GetDocentById), new { id = gemaakteDocent.Id }, gemaakteDocent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocent(int id)
        {
            if (await _repository.GetDocentByIdAsync(id) == null)
            {
                return NotFound($"We vonden geen docent met Id {id}");
            }

            await _repository.DeleteDocentAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDocent(int id, Docent gewijzigdDocent)
        {
            if (await _repository.GetDocentByIdAsync(id) == null)
            {
                return NotFound($"We vonden geen docent met Id {id}");
            }

            await _repository.UpdateDocentAsync(id, gewijzigdDocent);

            return NoContent();
        }
    }
}