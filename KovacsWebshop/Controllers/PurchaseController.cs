using KovacsWebshop.DAL;
using KovacsWebshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KovacsWebshop.Controllers
{
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly ProductDbContext _dbContext;

        public PurchaseController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var userEmail = User.Identity?.Name;

            var purchases = _dbContext.Purchases
                .Where(p => p.Email == userEmail)
                .OrderByDescending(p => p.PurchaseDate)
                .ToList();
            return View(purchases);
        }

        [HttpGet]
        public IActionResult Create(int? productId)
        {
            var userEmail = User.Identity?.Name;

            var purchase = new Purchase
            {
                PurchaseDate = DateTime.Now,
                Status = OrderStatus.Pending,
                Quantity = 1,
                Email = userEmail ?? string.Empty
            };

            if (productId.HasValue)
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.ProductId == productId.Value);
                if (product != null)
                {
                    purchase.ProductName = product.Name;
                    purchase.Price = product.Price;
                }
            }

            return View(purchase);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Purchase purchase)
        {
            var userEmail = User.Identity?.Name;
            purchase.Email = userEmail ?? string.Empty;

            if (!ModelState.IsValid)
            {
                return View(purchase);
            }

            purchase.PurchaseDate = DateTime.Now;
            _dbContext.Purchases.Add(purchase);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Confirmation), new { id = purchase.PurchaseId });
        }

        public IActionResult Confirmation(int id)
        {
            var purchase = _dbContext.Purchases.FirstOrDefault(p => p.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }
    }
}
