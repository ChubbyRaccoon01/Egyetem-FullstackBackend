using KovacsWebshop.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KovacsWebshop.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ProductDbContext _dbContext;

        public NavigationMenuViewComponent(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            // (Kard, Kés, Fejsze)
            var categories = _dbContext.Products
                .Select(p => p.Type)
                .Where(t => t != null)
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            return View(categories);
        }
    }
}