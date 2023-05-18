using Exercise3.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Exercise3.Services
{
    public interface IFileDbService
    {
        public IEnumerable<Student> Students { get; set; }
        Task SaveChanges();
    }

    public class FileDbService : IFileDbService
    {
        private readonly string _pathToFileDatabase;
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
        public FileDbService(IConfiguration configuration)
        {
            _pathToFileDatabase = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException(nameof(configuration));
            Initialize();
        }

        private void Initialize()
        {
            if (!File.Exists(_pathToFileDatabase))
            {
                return;
            }
            var lines = File.ReadLines(_pathToFileDatabase);

            var students = new List<Student>();

            lines.ToList().ForEach(line =>
            {
                var splittedLine = line.Split(',');
                var newStudent = new Student
                {
                    
                    FirstName = splittedLine[0],
                    LastName = splittedLine[1],
                    StudyName = splittedLine[2],
                    StudyMode = splittedLine[3],
                    IndexNumber = splittedLine[4],
                    BirthDate = splittedLine[5],
                    Email = splittedLine[6],
                    MothersName = splittedLine[7],
                    FathersName = splittedLine[8],
                };

                students.Add(newStudent);
            });


            Students = students;
        }

        public async Task SaveChanges()
        {
            List<string> students = new List<string>();
            foreach (var student in Students)
            {
                students.Add(student.ToString());
            }
            await File.WriteAllLinesAsync(
                _pathToFileDatabase,
                students,
                System.Text.Encoding.UTF8
                //tutaj należy zapewnić listę stringów zawierającą odpowiednio sformatowane dane studentów
                     // np. Jan,Kowalski,s1234,3/20/1991,Informatyka,Dzienne,kowalski@wp.pl,Jan,Anna
                );
        }

    }
   
}
