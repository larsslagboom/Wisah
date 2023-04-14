using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Wisah_webshops.Models
{
    public class orderViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
