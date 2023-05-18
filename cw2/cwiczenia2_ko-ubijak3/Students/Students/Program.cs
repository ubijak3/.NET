
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 4) {
            throw new ArgumentOutOfRangeException();
        }
        var csvPath = args[0];
        if (!File.Exists(csvPath))
        {
            throw new FileNotFoundException();
        };
        var csvContent = File.ReadLines(csvPath);
        var output = args[1];
        if (!Directory.Exists(output))
        {
            throw new DirectoryNotFoundException();
        };
        if (!File.Exists(args[2])){
            throw new FileNotFoundException();
        }
        var logs = File.CreateText(args[2]);
        var format = args[3];
        if (!format.Equals("json")) {
            throw new InvalidOperationException();
        }

        var students = new List<Student>();
        var dict = new Dictionary<string, int>();

        csvContent.ToList().ForEach(line =>
        {
            var splittedLine = line.Split(',');
            if (splittedLine.Length != 9)
            {
                logs.WriteLine($"Wiersz nie posiada odpowiedniej ilości kolumn: {line}");
                return;
            }
            if (splittedLine.Any(e => e.Trim() == ""))
            {
                logs.WriteLine($"Wiersz nie może posiadać pustych kolumn: {line}");
                return;
            }

            var newStudies = new Studies
            {
                Name = splittedLine[2],
                Mode = splittedLine[3],
            };

            var newStudent = new Student
            {
                IndexNumber = splittedLine[4],
                Fname = splittedLine[0],
                Lname = splittedLine[1],
                Birthdate = DateTime.Parse(splittedLine[5]),
                Email = splittedLine[6],
                MothersName = splittedLine[7],
                FathersName = splittedLine[8],
                Studies = newStudies
            };

            if (students.Any(e => e.IndexNumber == newStudent.IndexNumber && e.Fname == newStudent.Fname && e.Lname == newStudent.Lname))
            {
                logs.WriteLine($"Duplikat: {line}");
                return;
            }

            students.Add(newStudent);
            dict[newStudies.Name] = !dict.ContainsKey(newStudies.Name) ? 1 : dict[newStudies.Name] + 1;
        });

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        options.Converters.Add(new CustomDateTimeConverter("dd.MM.yyyy"));


        //D:/DotNetProjects/Students/Students/uczelnia.json
        File.WriteAllText(output + "/uczelnia.json", JsonSerializer.Serialize(
            new UczelniaWrapper
            {
                Uczelnia = new Uczelnia
                {
                    CreatedAt = DateTime.Now,
                    Author = "Marcin Kubiak",
                    Students = students,
                    ActiveStudies = dict.Select(e => new ActiveStudies { Name = e.Key, NumberOfStudents = e.Value })
                }
            },
            options

        ));

        logs.Flush();
    }
}

class Student
{
    public string IndexNumber { get; set; }
    public string Fname { get; set; }
    public string Lname { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; }
    public string MothersName { get; set; }
    public string FathersName { get; set; }
    public Studies Studies { get; set; }
}

class Studies
{
    public string Name { get; set; }
    public string Mode { get; set; }
}

class Uczelnia
{
    public DateTime CreatedAt { get; set; }
    public string Author { get; set; }
    public IEnumerable<Student> Students { get; set; }
    public IEnumerable<ActiveStudies> ActiveStudies { get; set; }
}

class ActiveStudies
{
    public string Name { get; set; }
    public int NumberOfStudents { get; set; }
}

class UczelniaWrapper
{
    public Uczelnia Uczelnia { get; set; }
}

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    private readonly string Format;
    public CustomDateTimeConverter(string format)
    {
        Format = format;
    }
    public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
    {
        writer.WriteStringValue(date.ToString(Format));
    }
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), Format, null);
    }
};
