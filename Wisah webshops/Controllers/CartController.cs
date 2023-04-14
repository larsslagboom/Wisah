using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Wisah_webshops.Models;

namespace Wisah_webshops.Controllers
{
    public class CartController : Controller
    {
        private readonly WebshopContext _context;

        public CartController(WebshopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<int> cart;
            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartBytes))
            {
                cart = JsonSerializer.Deserialize<List<int>>(cartBytes);
            }
            else
            {
                cart = new List<int>();
            }

            var products = _context.Products
                .Where(p => cart.Contains(p.Id))
                .ToList();

            var model = new CartViewModel
            {
                Products = products,
                TotalPrice = products.Sum(p => p.Price)
            };

            return View(model);
        }

        public IActionResult AddToCart(int id)
        {
            List<int> cart;
            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartBytes))
            {
                cart = JsonSerializer.Deserialize<List<int>>(cartBytes);
            }
            else
            {
                cart = new List<int>();
            }

            if (!cart.Contains(id))
            {
                cart.Add(id);
            }

            HttpContext.Session.Set("Cart", JsonSerializer.SerializeToUtf8Bytes(cart));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<int> cart;
            if (HttpContext.Session.TryGetValue("Cart", out byte[] cartBytes))
            {
                cart = JsonSerializer.Deserialize<List<int>>(cartBytes);
            }
            else
            {
                cart = new List<int>();
            }

            cart.Remove(id);

            HttpContext.Session.Set("Cart", JsonSerializer.SerializeToUtf8Bytes(cart));

            return RedirectToAction(nameof(Index));
        }
    }

}
