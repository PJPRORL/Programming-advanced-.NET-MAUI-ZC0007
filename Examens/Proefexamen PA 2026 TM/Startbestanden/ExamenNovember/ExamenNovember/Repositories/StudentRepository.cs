namespace ExamenNovember.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ExamenNovemberContext _context;

        public StudentRepository(ExamenNovemberContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllStudentenAsync()
        {
            return _context.Studenten.OrderBy(student => student.Naam).ToList();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return _context.Studenten.Find(id);
        }

        public async Task<Student> CreateStudentAsync(Student student)
        {
            _context.Studenten.Add(student);
            _context.SaveChanges();
            return student;
        }

        public async Task DeleteStudentAsync(int id)
        {
            Student? student = _context.Studenten.Find(id);

            if (student != null)
            {
                _context.Studenten.Remove(student);
                _context.SaveChanges();
            }
        }

        public async Task UpdateStudentAsync(int id, Student student)
        {
            Student? bestaandeStudent = _context.Studenten.Find(id);

            if (bestaandeStudent != null)
            {
                bestaandeStudent.Naam = student.Naam;
                bestaandeStudent.Voornaam = student.Voornaam;
                _context.SaveChanges();
            }
        }
    }
}
