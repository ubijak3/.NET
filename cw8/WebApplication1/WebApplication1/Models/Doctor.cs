using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Doctor
    {
        [Key]
        public int IdDoctor { get; set; }
        [Required]
        [MaxLength(100)]
        public String FirstName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public String LastName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public String Email { get; set; } = null!;
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
