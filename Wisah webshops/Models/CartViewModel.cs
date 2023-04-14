using Microsoft.AspNetCore.Mvc;

namespace Wisah_webshops.Models
{
    public class CartViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
