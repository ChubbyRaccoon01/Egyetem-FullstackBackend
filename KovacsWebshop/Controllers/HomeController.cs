using System.Diagnostics;
using KovacsWebshop.DAL;
using KovacsWebshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace KovacsWebshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ProductDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ProductDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(string? category, string? search)
        {
            var products = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Type == category);
            }

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }

            ViewBag.SelectedCategory = category;
            ViewBag.SearchTerm = search;
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
