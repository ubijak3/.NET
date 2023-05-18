using Exercise3.Models;
using Exercise3.Services;

namespace Exercise3.Repositories
{
    public interface IStudentsRepository
    {
        IEnumerable<Student> GetStudents();
        Task DeleteStudent (Student student);
        Task AddStudent(Student student);
        Task UpdateStudent(Student student, Student newData);
    }

    public class StudentsRepository : IStudentsRepository
    {

        private readonly IFileDbService _fileDbService;

        public StudentsRepository(IFileDbService fileDbService)
        {
            _fileDbService = fileDbService;
        }

        public IEnumerable<Student> GetStudents()
        {
            return _fileDbService.Students;
        }

        public async Task DeleteStudent(Student student)
        {
            ((List<Student>)_fileDbService.Students).Remove(student);
            await _fileDbService.SaveChanges();
        }

        public async Task AddStudent(Student student)
        {
            ((List<Student>)_fileDbService.Students).Add(student);
            await _fileDbService.SaveChanges();
        }

        public async Task UpdateStudent(Student student, Student newData)
        {
            var index = student.IndexNumber;
            student = newData;
            student.IndexNumber = index;
            await _fileDbService.SaveChanges();
        }
    }
}
