using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KovacsWebshop.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Product name")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(0.01, 10000000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [StringLength(100)]
        [Display(Name = "Product type")]
        public string? Type { get; set; }
    }
}
