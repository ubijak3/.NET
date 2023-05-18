using System.ComponentModel.DataAnnotations;

namespace Exercise3.Models.DTOs
{
    public class StudentPOST
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string IndexNumber { get; set; } = string.Empty;
        [Required]
        public string BirthDate { get; set; } = string.Empty;
        [Required]
        public string StudyName { get; set; } = string.Empty;
        [Required]
        public string StudyMode { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FathersName { get; set; } = string.Empty;
        [Required]
        public string MothersName { get; set; } = string.Empty;
    }
}
