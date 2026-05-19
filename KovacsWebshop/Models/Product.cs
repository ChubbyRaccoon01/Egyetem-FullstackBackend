using System.ComponentModel.DataAnnotations.Schema;

namespace KovacsWebshop.Models
{
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? Type { get; set; }

    }
}
