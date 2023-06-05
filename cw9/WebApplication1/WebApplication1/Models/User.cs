using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? RefeshTokenExp { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;
        [Required]
        public byte[] Salt { get; set; }
    }
}
