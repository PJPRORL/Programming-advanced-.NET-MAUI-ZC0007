using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class BestellingContext : DbContext
{
    public BestellingContext(DbContextOptions<BestellingContext> options)
        : base(options) { }

    public DbSet<Bestelling> Bestellingen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bestelling>(entity =>
        {
            entity.ToTable("Bestellingen");

            entity.Property(p => p.KlantNaam).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Gerechten).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Status).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Tafelnummer).IsRequired();
            entity.Property(p => p.TotaalPrijs).IsRequired().HasPrecision(18, 2);
        });

        modelBuilder.Entity<Bestelling>().HasData(
            new Bestelling { Id = 1, KlantNaam = "Jansen", Tafelnummer = 5, Gerechten = "Lasagne-Salade", Status = "Klaar", TotaalPrijs = 24.50 },
            new Bestelling { Id = 2, KlantNaam = "De Smet", Tafelnummer = 12, Gerechten = "Risotto-Wijn", Status = "In bereiding", TotaalPrijs = 32.00 },
            new Bestelling { Id = 3, KlantNaam = "Peeters", Tafelnummer = 3, Gerechten = "Carpaccio-Koffie", Status = "Afgehaald", TotaalPrijs = 18.75 },
            new Bestelling { Id = 4, KlantNaam = "Willems", Tafelnummer = 8, Gerechten = "Gegrilde Kabeljauw-Dessert", Status = "Betaald", TotaalPrijs = 45.00 },
            new Bestelling { Id = 5, KlantNaam = "Claes", Tafelnummer = 1, Gerechten = "Stoofvlees-Bier", Status = "In bereiding", TotaalPrijs = 28.50 },
            new Bestelling { Id = 6, KlantNaam = "Dubois", Tafelnummer = 15, Gerechten = "Moules-Frites", Status = "Klaar", TotaalPrijs = 22.00 }
        );
    }
}
