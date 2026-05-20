using Microsoft.EntityFrameworkCore;
using KovacsWebshop.DAL;
using KovacsWebshop.Models;
using Microsoft.AspNetCore.Identity;

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
                        Name = "XIV-es típusú lovagkard (Oakeshott XIV)",
                        Description = "14. századi egykezes lovagkard, rombusz keresztmetszetű pengével és keresztvasas kézfejvédelemmel. Kiváló egyensúlyú, szúrásra és vágásra egyaránt alkalmas fegyver.",
                        Price = 89000m,
                        Type = "Kard"
                    },
                    new Product
                    {
                        Name = "Viking szász (Seax)",
                        Description = "9. századi viking egyélű nagykés, aszimmetrikus pengével és faragott csontmarkolattal. Közelharci és hétköznapi eszközként egyaránt használták.",
                        Price = 125000m,
                        Type = "Kard"
                    },
                    new Product
                    {
                        Name = "Hosszú tőr (Baselard)",
                        Description = "Svájci és észak-itáliai városokban elterjedt 14–15. századi kétélű tőr, H-alakú markolattal. Polgárok és zsoldosok körében egyaránt népszerű közelharcfegyver volt.",
                        Price = 45000m,
                        Type = "Kés"
                    },
                    new Product
                    {
                        Name = "Vadászkés (Bowie-kés)",
                        Description = "Jim Bowie texasi határvédőről elnevezett 19. századi amerikai vadászkés, szegett hátú pengével (clip point) és rézgárdával. Az amerikai Nyugat jelképes fegyvere.",
                        Price = 32000m,
                        Type = "Kés"
                    },
                    new Product
                    {
                        Name = "Dán fejsze (Dane Axe)",
                        Description = "10–11. századi skandináv és angol kétkezes harci fejsze, széles, íves pengével. A hastingsi csatában (1066) a housecarl gyalogosok jellegzetes fegyvere volt.",
                        Price = 68000m,
                        Type = "Fejsze"
                    },
                    new Product
                    {
                        Name = "Lovagi harci fejsze (Pollaxe)",
                        Description = "15. századi nehézgyalogsági fejsze, fejszepengével, tüskés kalapácsfejjel és nyéltüskével (langet). Páncélos ellenfél ellen fejlesztett tornai és csatafegyver.",
                        Price = 112000m,
                        Type = "Fejsze"
                    },
                    new Product
                    {
                        Name = "Szakállas fejsze (Skeggjøx)",
                        Description = "Viking kori egykezes fejsze, lefelé nyúló, kampós pengével, amely a pajzs mögül is elérte az ellenfelet. A 8–10. századi skandináv harcosok egyik leggyakoribb fegyvere.",
                        Price = 54000m,
                        Type = "Fejsze"
                    }
                );
                context.SaveChanges();
            }
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                context.Roles.Add(adminRole);
                context.SaveChanges();
            }
            if (!context.Users.Any(u => u.Email == "admin@forgelegacy.com"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@forgelegacy.com",
                    Email = "admin@forgelegacy.com",
                    EmailConfirmed = true
                };

                var hasher = new PasswordHasher<IdentityUser>();
                adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123$");

                context.Users.Add(adminUser);
                context.SaveChanges();

                var admin = context.Users.FirstOrDefault(u => u.Email == "admin@forgelegacy.com");
                var role = context.Roles.FirstOrDefault(r => r.Name == "Admin");

                if (admin != null && role != null)
                {
                    context.UserRoles.Add(new IdentityUserRole<string>
                    {
                        UserId = admin.Id,
                        RoleId = role.Id
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}