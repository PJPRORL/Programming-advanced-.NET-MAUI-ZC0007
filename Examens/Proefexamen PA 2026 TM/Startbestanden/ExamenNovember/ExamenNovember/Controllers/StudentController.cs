using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamenNovember.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Student>>> GetAllStudenten()
        {
            return Ok(await _repository.GetAllStudentenAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            Student? student = await _repository.GetStudentByIdAsync(id);

            if (student == null)
            {
                return NotFound($"We vonden geen student met Id: {id}");
            }

            return Ok(student);
        }

        [HttpPost()]
        public async Task<ActionResult<Student>> CreateDocent(Student nieuweStudent)
        {
            Student gemaakteStudent = await _repository.CreateStudentAsync(nieuweStudent);

            return CreatedAtAction(nameof(GetStudentById), new { id = gemaakteStudent.Id }, gemaakteStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (await _repository.GetStudentByIdAsync(id) == null)
            {
                return NotFound($"We vonden geen student met Id {id}");
            }

            await _repository.DeleteStudentAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, Student gewijzigdStudent)
        {
            if (await _repository.GetStudentByIdAsync(id) == null)
            {
                return NotFound($"We vonden geen student met Id {id}");
            }

            await _repository.UpdateStudentAsync(id, gewijzigdStudent);

            return NoContent();
        }
    }
}
