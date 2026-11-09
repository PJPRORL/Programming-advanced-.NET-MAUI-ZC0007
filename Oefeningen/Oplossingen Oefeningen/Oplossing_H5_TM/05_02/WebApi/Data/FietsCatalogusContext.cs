using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class FietsCatalogusContext : DbContext
{
    public FietsCatalogusContext(DbContextOptions<FietsCatalogusContext> options)
        : base(options) { }

    public DbSet<Fiets> Fietsen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Fiets>(entity =>
        {
            entity.ToTable("Fietsen");

            entity.Property(p => p.Merk).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Type).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Kleur).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Prijs).IsRequired();
            entity.Property(p => p.Aantal).IsRequired();
        });

        modelBuilder.Entity<Fiets>().HasData(
            new Fiets { Id = 1, Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 },
            new Fiets { Id = 2, Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 },
            new Fiets { Id = 3, Merk = "Gazelle", Type = "Stadsfiets", Kleur = "Blauw", Prijs = 800.00, Aantal = 12 },
            new Fiets { Id = 4, Merk = "Trek", Type = "Racefiets", Kleur = "Groen", Prijs = 1800.00, Aantal = 0 },
            new Fiets { Id = 5, Merk = "Giant", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1500.00, Aantal = 7 },
            new Fiets { Id = 6, Merk = "Koga", Type = "Stadsfiets", Kleur = "Wit", Prijs = 950.00, Aantal = 8 }
        );
    }
}
