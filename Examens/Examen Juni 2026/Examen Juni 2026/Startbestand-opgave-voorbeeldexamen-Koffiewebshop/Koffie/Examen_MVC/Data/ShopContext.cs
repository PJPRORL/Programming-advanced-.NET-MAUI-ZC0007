using Examen_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Examen_MVC.Data
{
    public class ShopContext(DbContextOptions<ShopContext> options) : IdentityDbContext<CustomUser>(options)
    {
        public DbSet<Brewer> Brewers { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Coffee> Coffees { get; set; }

        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            SetTableNames(builder);
            SetTableRelations(builder);
            SeedDummyData(builder);
        }

        private void SetTableNames(ModelBuilder builder)
        {
            builder.Entity<Brewer>().ToTable("Brewers");
            builder.Entity<Cart>().ToTable("Carts");
            builder.Entity<Coffee>().ToTable("Coffees");
            builder.Entity<Country>().ToTable("Countries");
        }

        private void SetTableRelations(ModelBuilder builder)
        {
            builder.Entity<Coffee>()
                .HasOne(x => x.Brewer)
                .WithMany(x => x.Coffees)
                .HasForeignKey(x => x.BrewerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Country>()
                .HasMany(x => x.Brewers)
                .WithOne(x => x.Country)
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cart>()
                .HasOne(x => x.User)
                .WithMany(x => x.ItemsInCart)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Cart>()
                .HasOne(x => x.Coffee)
                .WithMany(x => x.CoffeesInCart)
                .HasForeignKey(x => x.CoffeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void SeedDummyData(ModelBuilder builder)
        {
            builder.Entity<Country>()
                .HasData(new List<Country>()
                {
                    new Country
                    {
                        ID = 1,
                        Name = "Belgium"
                    },
                    new Country
                    {
                        ID = 2,
                        Name = "Ethiopia"
                    },
                    new Country
                    {
                        ID = 3,
                        Name = "Indonesia"
                    },
                    new Country
                    {
                        ID = 4,
                        Name = "Brazil"
                    },
                    new Country
                    {
                        ID = 5,
                        Name = "Colombia"
                    },
                });

            builder.Entity<Brewer>()
                .HasData(new List<Brewer>() {
                    new Brewer
                    {
                        Id = 1,
                        Name = "Douwe Egberts",
                        CountryId = 1,
                    },
                    new Brewer
                    {
                        Id = 2,
                        Name = "Ethiopia Brewing Co.",
                        CountryId = 2,
                    },
                    new Brewer
                    {
                        Id = 3,
                        Name = "Java",
                        CountryId = 3,
                    },
                });

            builder.Entity<Coffee>()
                .HasData(new List<Coffee>() {
                    new Coffee
                    {
                        Id = 1,
                        BrewerId = 1,
                        Name = "Christmas Antigua",
                        Description = "An elegant, complex coffee with great depth and subtle cocoa and spice flavours.",
                        Price = 7.99,
                        Image = "christmas.jpg"
                    },
                    new Coffee
                    {
                        Id = 2,
                        BrewerId = 2,
                        Name = "Colombia",
                        Description = "Our tribute to the birthplace of coffee.",
                        Price = 4.99,
                        Image = "colombia.jpg"
                    },
                    new Coffee
                    {
                        Id = 3,
                        BrewerId = 3,
                        Name = "Guatamala smooth",
                        Description = "A medium-bodied, multi-region blend, roasted dark.",
                        Price = 12.99,
                        Image = "guatamala.jpg"
                    },
                    new Coffee
                    {
                        Id = 4,
                        BrewerId = 1,
                        Name = "Italian Roast",
                        Description = "A medium-roasted coffee with rich flavour.",
                        Price = 4.99,
                        Image = "italianRoast.jpg"
                    },
                    new Coffee
                    {
                        Id = 5,
                        BrewerId = 2,
                        Name = "Columbia",
                        Description = "Our tribute to the birthplace of coffee.",
                        Price = 4.99,
                        Image = "pikePlace.jpg"
                    },
                    new Coffee
                    {
                        Id = 6,
                        BrewerId = 3,
                        Name = "Guatamala smooth",
                        Description = "A medium-bodied, multi-region blend, roasted dark.",
                        Price = 12.99,
                        Image = "willowBlend.jpg"
                    },
                });
        }
    }
}