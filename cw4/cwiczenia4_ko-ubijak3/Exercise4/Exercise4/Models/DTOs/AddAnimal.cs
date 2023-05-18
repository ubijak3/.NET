using System.ComponentModel.DataAnnotations;

namespace Exercise4.Models.DTOs
{
    public class AddAnimal
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = String.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(200)]
        public string Category { get; set; } = String.Empty;

        [Required]
        [MaxLength(200)]
        public string Area { get; set; } = String.Empty;
    }
}
