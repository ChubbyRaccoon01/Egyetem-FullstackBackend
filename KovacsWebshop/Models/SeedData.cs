using Microsoft.EntityFrameworkCore;
using KovacsWebshop.DAL;
using KovacsWebshop.Models;

namespace KovacsWebshop.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ProductDbContext context = app.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ProductDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "XIV-es típusú lovagkard",
                        Description = "14. századi egykezes lovagkard, keresztvasas védelemmel, faragott fanyelű. Kiváló egyensúlyú fegyver.",
                        Price = 89000m,
                        Type = "Kard"
                    },
                    new Product
                    {
                        Name = "Viking szász",
                        Description = "9. századi viking egyélű kard, gazdagon díszített markolattal, vérárok a penge közepén.",
                        Price = 125000m,
                        Type = "Kard"
                    },
                    new Product
                    {
                        Name = "Hosszú tőr (Baselard)",
                        Description = "Svájci eredetű, 14-15. századi kétélű tőr, kiváló szúrófegyver a közelharcban.",
                        Price = 45000m,
                        Type = "Kés"
                    },
                    new Product
                    {
                        Name = "Vadászkés (Bowie)",
                        Description = "19. századi amerikai vadászkés, erős gerincű, fűrészes hátú kivitelben. Tökéletes túrázáshoz.",
                        Price = 32000m,
                        Type = "Kés"
                    },
                    new Product
                    {
                        Name = "Dárdás fejsze (Pernach)",
                        Description = "Kelet-európai eredetű lovas fejsze, kalapácsfejjel a másik oldalon. Hatékony páncél ellen.",
                        Price = 68000m,
                        Type = "Fejsze"
                    },
                    new Product
                    {
                        Name = "Kampós fejsze (Pollaxe)",
                        Description = "Kétkezes lovagi fejsze, kampós véggel a lóról való lehúzáshoz. 15. századi eredetű.",
                        Price = 112000m,
                        Type = "Fejsze"
                    },
                    new Product
                    {
                        Name = "Skandináv sörényvágó",
                        Description = "Viking kori egykezes fejsze, könnyű és gyors, a bal kézben pajzs mellett használták.",
                        Price = 54000m,
                        Type = "Fejsze"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}