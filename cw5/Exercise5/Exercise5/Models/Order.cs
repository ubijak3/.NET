using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Exercise5.Models
{
    public class Order
    {
        [Required]
        public int IdOrder { get; set; }
        [Required]
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        [AllowNull]
        public DateTime FulfilledAt { get; set; }
    }
}
