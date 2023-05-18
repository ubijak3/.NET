using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        [Required]
        [MaxLength(100)]
        public String Name { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public String Description { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public String Type{ get; set; } = null!;
        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
