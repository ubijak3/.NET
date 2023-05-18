using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class DoctorPUT
    {
        [Required]
        [MaxLength(100)]
        public String FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public String Email { get; set; }   
    }
}