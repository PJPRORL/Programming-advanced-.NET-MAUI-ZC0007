using Examen_MVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;

namespace Examen_MVC.Data
{
    public class IdentitySeeding
    {
        public async Task IdentitySeedingAsync(UserManager<CustomUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                // Gebruiker aanmaken
                // Admin bestaat nog niet?
                if (userManager.FindByNameAsync("Admin").Result == null)
                {
                    // Gebruikers voorzien
                    CustomUser gebruiker = new CustomUser
                    {
                        UserName = "User",
                        Achternaam = " De Gebruiker",
                        Voornaam = "Jos",
                        Geboortedatum = DateTime.Now,
                        Email = "user@mvc.be",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };

                    CustomUser admin = new CustomUser
                    {
                        UserName = "Admin",
                        Achternaam = " De Beheerder",
                        Voornaam = "Admin",
                        Geboortedatum = DateTime.Now,
                        Email = "admin@mvc.be",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };

                    CustomUser superAdmin = new CustomUser
                    {
                        UserName = "SuperAdmin",
                        Achternaam = " De Super Beheerder",
                        Voornaam = "Super",
                        Geboortedatum = DateTime.Now,
                        Email = "superadmin@mvc.be",
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                    };

                    // Gebruikers aanmaken
                    await userManager.CreateAsync(gebruiker, "#WachtWoord123#");
                    await userManager.CreateAsync(admin, "#WachtWoord123#");
                    await userManager.CreateAsync(superAdmin, "#WachtWoord123#");

                    // Rollen seeden
                    string[] roles = ["Admin", "SuperAdmin"];

                    foreach (string role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }

                    await userManager.AddToRoleAsync(admin, "admin");
                    await userManager.AddToRoleAsync(superAdmin, "superadmin");
                }
            }
            catch (DbException ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}