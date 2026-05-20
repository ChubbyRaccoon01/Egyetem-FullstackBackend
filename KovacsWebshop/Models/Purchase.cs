using System.ComponentModel.DataAnnotations;

namespace KovacsWebshop.Models
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        PurchaseBooked,
        Delivered
    }

    public class Purchase
    {
        public int PurchaseId { get; set; }

        [Required]
        [Display(Name = "Product name")]
        public string ProductName { get; set; } = null!;

        [Required]
        [Range(0.01, 10000000)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 9999)]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Purchase date")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; } = null!;

        [Required]
        [Display(Name = "Order status")]
        public OrderStatus Status { get; set; }
    }
}
