using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wisah_webshops.Models;

namespace Wisah_webshops.Controllers
{
    public class OrdersController : Controller
    {
        private readonly WebshopContext _context;

        public OrdersController(WebshopContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Submit(orderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                var order = new orderViewModel
                {
                    CustomerName = orderViewModel.CustomerName,
                    CustomerEmail = orderViewModel.CustomerEmail,
                    ShippingAddress = orderViewModel.ShippingAddress,
                    TotalPrice = orderViewModel.TotalPrice,
                    OrderDate = orderViewModel.OrderDate,
                    OrderItems = orderViewModel.OrderItems.Select(orderItemViewModel =>
                        new OrderItem
                        {
                            ProductId = orderItemViewModel.ProductId,
                            ProductName = orderItemViewModel.ProductName,
                            Quantity = orderItemViewModel.Quantity,
                            Price = orderItemViewModel.Price
                        }).ToList()
                };

                _context.Orders.Add(order);
                _context.SaveChanges();

                HttpContext.Session.Clear();

                return RedirectToAction("ThankYou");
            }
            else
            {
                return View("Create", orderViewModel);
            }
        }

        public IActionResult Confirmation(int id)
        {
            var order = _context.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);
            return View(order);
        }
    }

}
