using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenMuziekschool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LesgeverController : ControllerBase
    {
        private readonly ILesgeverRepository _lesgeverRepository;

        public LesgeverController(ILesgeverRepository lesgeverRepository)
        {
            _lesgeverRepository = lesgeverRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Lesgever>>> GetAllLesgevers()
        {
            return Ok(await _lesgeverRepository.GetAllLesgeversAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesgever>> GetLesgeverById(int id)
        {
            Lesgever? lesgever = await _lesgeverRepository.GetLesgeverByIdAsync(id);

            if (lesgever == null)
            {
                return NotFound($"Kon geen lesgever vinden met id: {id}");
            }

            return Ok(lesgever);
        }

        [HttpPost()]
        public async Task<ActionResult<Lesgever>> CreateLesgever(Lesgever nieuweLesgever)
        {
            Lesgever? gemaaktelesgever = await _lesgeverRepository.CreateLesgeverAsync(nieuweLesgever);

            //if (gemaaktelesgever.Naam == "")
            //{
            //    return NotFound("De naam van de lesgever mag niet leeg zijn.");
            //}

            return CreatedAtAction(nameof(GetLesgeverById), new { id = gemaaktelesgever.Id }, gemaaktelesgever);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Lesgever>> WijzigLesgever(int id, Lesgever gewijzigdeLesgever)
        {
            if (await _lesgeverRepository.GetLesgeverByIdAsync(id) == null)
            {
                return NotFound($"Kon geen lesgever vinden met id: {id}");
            }

            await _lesgeverRepository.WijzigLesgeverAsync(id, gewijzigdeLesgever);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Lesgever>> VerwijderLesgever(int id)
        {
            if (await _lesgeverRepository.GetLesgeverByIdAsync(id) == null)
            {
                return NotFound($"Kon geen lesgever vinden met id: {id}");
            }

            await _lesgeverRepository.DeleteLesgeverAsync(id);
            return NoContent();
        }
    }
}
