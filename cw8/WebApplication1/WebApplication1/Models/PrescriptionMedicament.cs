using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{

    [PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
    public class PrescriptionMedicament
    {
        public int IdMedicament { get; set; }
        public int IdPrescription { get; set; }
        public int? Dose { get; set; }
        [Required]
        [MaxLength(100)]
        public String Details { get; set; } = null!;
        [ForeignKey(nameof(IdMedicament))]
        public virtual Medicament Medicament { get; set; } = null!;
        [ForeignKey(nameof(IdPrescription))]
        public virtual Prescription Prescription { get; set; } = null!;
    }
}
