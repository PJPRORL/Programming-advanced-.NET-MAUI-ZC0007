namespace ExamenNovember.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudentenAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task UpdateStudentAsync(int id, Student student);
        Task<Student> CreateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
