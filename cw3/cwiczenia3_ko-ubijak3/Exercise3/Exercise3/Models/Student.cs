namespace Exercise3.Models
{
    public class Student
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string IndexNumber { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string StudyName { get; set; } = string.Empty;
        public string StudyMode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FathersName { get; set; } = string.Empty;
        public string MothersName { get; set; } = string.Empty;

        public override string? ToString()
        {
            return FirstName + "," + LastName + "," + IndexNumber + "," + BirthDate + "," + StudyName + "," + StudyMode + "," + Email + "," + FathersName + "," + MothersName;
        }
    }

    
}
