using KovacsWebshop.DAL;
using KovacsWebshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KovacsWebshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderHandlingController : Controller
    {
        private readonly ProductDbContext _dbContext;

        public OrderHandlingController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var orders = _dbContext.Purchases
                .OrderByDescending(o => o.PurchaseDate)
                .ToList();
            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, OrderStatus status)
        {
            var order = _dbContext.Purchases.Find(id);
            if (order != null)
            {
                order.Status = status;
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}