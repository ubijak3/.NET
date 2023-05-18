using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Patient
    {
        [Key]
        public int IdPatient { get; set; }
        [Required]
        [MaxLength(100)]
        public String FirstName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public String LastName { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
