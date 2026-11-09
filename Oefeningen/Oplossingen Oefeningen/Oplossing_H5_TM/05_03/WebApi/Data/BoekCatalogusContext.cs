using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class BoekCatalogusContext : DbContext
{
    public BoekCatalogusContext(DbContextOptions<BoekCatalogusContext> options)
        : base(options) { }

    public DbSet<Boek> Boeken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Boek>(entity =>
        {
            entity.ToTable("Boeken");

            entity.Property(p => p.Titel).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Auteur).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Genre).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Prijs).IsRequired();
            entity.Property(p => p.Uitgeverij).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Voorraad).IsRequired();
        });

        modelBuilder.Entity<Boek>().HasData(
            new Boek { Id = 1, Titel = "De Kameleon", Auteur = "Jeroen Olyslagers", Genre = "Thriller", Prijs = 18.99, Uitgeverij = "Lannoo", Voorraad = 15 },
            new Boek { Id = 2, Titel = "De Avonturen van Pi", Auteur = "Yann Martel", Genre = "Avontuur", Prijs = 14.50, Uitgeverij = "Ambo|Anthos", Voorraad = 8 },
            new Boek { Id = 3, Titel = "Harry Potter en de Steen der Wijzen", Auteur = "J.K. Rowling", Genre = "Fantasy", Prijs = 19.99, Uitgeverij = "Uitgeverij Contact", Voorraad = 25 },
            new Boek { Id = 4, Titel = "Dune", Auteur = "Frank Herbert", Genre = "Sci-Fi", Prijs = 22.00, Uitgeverij = "Uitgeverij Lannoo", Voorraad = 12 },
            new Boek { Id = 5, Titel = "Het diner", Auteur = "Herman Koch", Genre = "Thriller", Prijs = 16.75, Uitgeverij = "Uitgeverij Prometheus", Voorraad = 6 },
            new Boek { Id = 6, Titel = "De Ontdekking van de Hemel", Auteur = "Harry Mulisch", Genre = "Romantiek", Prijs = 20.50, Uitgeverij = "Uitgeverij Contact", Voorraad = 4 },
            new Boek { Id = 7, Titel = "De Eenzaamheid van de Sierlijke", Auteur = "Amélie Nothomb", Genre = "Romantiek", Prijs = 15.00, Uitgeverij = "Uitgeverij Lannoo", Voorraad = 10 }
        );
    }
}
