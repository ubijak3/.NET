using System.ComponentModel.DataAnnotations;

namespace Exercise5.DTOs
{
    public class OrderPOST
    {
        [Required]
        public int idProduct { get; set; }
        [Required]
        public int idWarehouse { get; set; }
        [Required]
        public int amount { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
    }
}
