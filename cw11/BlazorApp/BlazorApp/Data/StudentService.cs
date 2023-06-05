namespace BlazorApp.Data
{

    public interface IStudentService {
        public ICollection<Student> GetStudents();
        public Student? GetStudent(int id);
        public bool RemoveStudent(int id);
    }
    public class StudentService : IStudentService
    {
        private ICollection<Student> _students;

        public StudentService() { 
            _students = new List<Student>
            {
                new Student
                {
                    IdStudent = 1,
                    FirstName = "test",
                    LastName = "test",
                    Birthdate = DateTime.Now,
                    Studies = "test",
                    AvatarUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/Sunflower_from_Silesia2.jpg/1200px-Sunflower_from_Silesia2.jpg"
                },
                new Student
                {
                    IdStudent = 2,
                    FirstName = "a",
                    LastName = "c",
                    Birthdate = DateTime.Now.AddDays(10),
                    Studies = "f",
                    AvatarUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/Sunflower_from_Silesia2.jpg/1200px-Sunflower_from_Silesia2.jpg"
                },
                new Student
                {
                    IdStudent = 2,
                    FirstName = "b",
                    LastName = "b",
                    Birthdate = DateTime.Now.AddHours(3),
                    Studies = "w",
                    AvatarUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/41/Sunflower_from_Silesia2.jpg/1200px-Sunflower_from_Silesia2.jpg"
                }
            };
        }

        public ICollection<Student> GetStudents()
        {
            return _students;
        }
        public Student? GetStudent(int id)
        {
            return _students.FirstOrDefault(e => e.IdStudent == id);
        }

        public bool RemoveStudent(int id)
        {
            var studentToRemove = _students.FirstOrDefault(e => e.IdStudent == id);
            if (studentToRemove != null)
            {
                _students.Remove(studentToRemove);
            }
            
            return true;
        }
    }
}
