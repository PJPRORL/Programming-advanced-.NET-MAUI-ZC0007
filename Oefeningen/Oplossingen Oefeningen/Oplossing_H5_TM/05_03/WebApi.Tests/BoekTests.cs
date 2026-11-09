using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests;

public class BoekEndpointsTests
{
    private SqliteConnection _connection = null!;
    private BoekCatalogusContext _context = null!;
    private IBoekRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<BoekCatalogusContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new BoekCatalogusContext(options);
        _context.Database.EnsureCreated();

        // Remove auto-applied seed data so we can insert our own
        _context.Boeken.RemoveRange(_context.Boeken);
        _context.SaveChanges();

        // Reset auto-increment counter in SQLite
        _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Boeken'");

        // Seed data
        _context.Boeken.AddRange(
            new Boek { Id = 1, Titel = "De Kameleon", Auteur = "Jeroen Olyslagers", Genre = "Thriller", Prijs = 18.99, Uitgeverij = "Lannoo", Voorraad = 15 },
            new Boek { Id = 2, Titel = "De Avonturen van Pi", Auteur = "Yann Martel", Genre = "Avontuur", Prijs = 14.50, Uitgeverij = "Ambo|Anthos", Voorraad = 8 },
            new Boek { Id = 3, Titel = "Harry Potter en de Steen der Wijzen", Auteur = "J.K. Rowling", Genre = "Fantasy", Prijs = 19.99, Uitgeverij = "Uitgeverij Contact", Voorraad = 25 },
            new Boek { Id = 4, Titel = "Dune", Auteur = "Frank Herbert", Genre = "Sci-Fi", Prijs = 22.00, Uitgeverij = "Uitgeverij Lannoo", Voorraad = 12 },
            new Boek { Id = 5, Titel = "Het diner", Auteur = "Herman Koch", Genre = "Thriller", Prijs = 16.75, Uitgeverij = "Uitgeverij Prometheus", Voorraad = 6 },
            new Boek { Id = 6, Titel = "De Ontdekking van de Hemel", Auteur = "Harry Mulisch", Genre = "Romantiek", Prijs = 20.50, Uitgeverij = "Uitgeverij Contact", Voorraad = 4 },
            new Boek { Id = 7, Titel = "De Eenzaamheid van de Sierlijke", Auteur = "Amélie Nothomb", Genre = "Romantiek", Prijs = 15.00, Uitgeverij = "Uitgeverij Lannoo", Voorraad = 10 }
        );
        _context.SaveChanges();

        _repository = new BoekRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }

    // --- CRUD-methodes ---

    [Test]
    public async Task GetAllAsync_GeeftAlleBoekenGesorteerdOpAuteur()
    {
        // Act
        var boeken = await _repository.GetAllAsync();

        // Assert
        Assert.That(boeken, Is.Not.Null);
        Assert.That(boeken.Count, Is.EqualTo(7));
        Assert.That(boeken[0].Auteur, Is.EqualTo("Amélie Nothomb"));
        Assert.That(boeken[1].Auteur, Is.EqualTo("Frank Herbert"));
        Assert.That(boeken[2].Auteur, Is.EqualTo("Harry Mulisch"));
        Assert.That(boeken[3].Auteur, Is.EqualTo("Herman Koch"));
        Assert.That(boeken[4].Auteur, Is.EqualTo("J.K. Rowling"));
        Assert.That(boeken[5].Auteur, Is.EqualTo("Jeroen Olyslagers"));
        Assert.That(boeken[6].Auteur, Is.EqualTo("Yann Martel"));
    }

    [Test]
    public async Task GetByIdAsync_GeeftCorrectBoekVoorBestaandeId()
    {
        // Act
        var boek = await _repository.GetByIdAsync(3);

        // Assert
        Assert.That(boek, Is.Not.Null);
        Assert.That(boek!.Titel, Is.EqualTo("Harry Potter en de Steen der Wijzen"));
        Assert.That(boek.Auteur, Is.EqualTo("J.K. Rowling"));
        Assert.That(boek.Genre, Is.EqualTo("Fantasy"));
        Assert.That(boek.Prijs, Is.EqualTo(19.99));
        Assert.That(boek.Uitgeverij, Is.EqualTo("Uitgeverij Contact"));
        Assert.That(boek.Voorraad, Is.EqualTo(25));
    }

    [Test]
    public async Task GetByIdAsync_GeeftNullVoorOnbestaandeId()
    {
        // Act
        var boek = await _repository.GetByIdAsync(99);

        // Assert
        Assert.That(boek, Is.Null);
    }

    [Test]
    public async Task CreateAsync_GeeftNieuwBoekMetToegewezenId()
    {
        // Arrange
        var nieuwBoek = new Boek { Titel = "Testboeken", Auteur = "Testauteur", Genre = "Test", Prijs = 10.00, Uitgeverij = "Test", Voorraad = 5 };

        // Act
        var result = await _repository.CreateAsync(nieuwBoek);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(8));
        Assert.That(result.Titel, Is.EqualTo("Testboeken"));
    }

    [Test]
    public async Task UpdateAsync_WerktBestaandBoekBij()
    {
        // Arrange
        var gewijzigdBoek = new Boek { Titel = "Gewijzigde titel", Auteur = "Gewijzigde auteur", Genre = "Gewijzigd genre", Prijs = 25.00, Uitgeverij = "Gewijzigde uitgeverij", Voorraad = 20 };

        // Act
        await _repository.UpdateAsync(1, gewijzigdBoek);

        // Assert
        var opgehaaldBoek = await _repository.GetByIdAsync(1);
        Assert.That(opgehaaldBoek, Is.Not.Null);
        Assert.That(opgehaaldBoek!.Titel, Is.EqualTo("Gewijzigde titel"));
        Assert.That(opgehaaldBoek.Auteur, Is.EqualTo("Gewijzigde auteur"));
        Assert.That(opgehaaldBoek.Genre, Is.EqualTo("Gewijzigd genre"));
        Assert.That(opgehaaldBoek.Prijs, Is.EqualTo(25.00));
        Assert.That(opgehaaldBoek.Uitgeverij, Is.EqualTo("Gewijzigde uitgeverij"));
        Assert.That(opgehaaldBoek.Voorraad, Is.EqualTo(20));
    }

    [Test]
    public async Task UpdateAsync_DoeNietsBijOnbestaandBoek()
    {
        // Arrange
        var gewijzigdBoek = new Boek { Titel = "Gewijzigde titel", Auteur = "Gewijzigde auteur", Genre = "Gewijzigd genre", Prijs = 25.00, Uitgeverij = "Gewijzigde uitgeverij", Voorraad = 20 };

        // Act
        await _repository.UpdateAsync(99, gewijzigdBoek);

        // Assert
        var opgehaaldBoek = await _repository.GetByIdAsync(1);
        Assert.That(opgehaaldBoek, Is.Not.Null);
        Assert.That(opgehaaldBoek!.Titel, Is.EqualTo("De Kameleon"));
    }

    [Test]
    public async Task DeleteAsync_VerwijdertBestaandBoek()
    {
        // Arrange
        Assert.That(await _repository.GetByIdAsync(1), Is.Not.Null);

        // Act
        await _repository.DeleteAsync(1);

        // Assert
        Assert.That(await _repository.GetByIdAsync(1), Is.Null);
        var boeken = await _repository.GetAllAsync();
        Assert.That(boeken.Count, Is.EqualTo(6));
    }

    [Test]
    public async Task DeleteAsync_DoeNietsBijOnbestaandBoek()
    {
        // Arrange
        var countBefore = (await _repository.GetAllAsync()).Count;

        // Act
        await _repository.DeleteAsync(99);

        // Assert
        var countAfter = (await _repository.GetAllAsync()).Count;
        Assert.That(countAfter, Is.EqualTo(countBefore));
    }

    // --- Query-methodes ---

    [Test]
    public async Task GetUniqueGenresAsync_GeeftUniekeGenresGesorteerd()
    {
        // Act
        var genres = await _repository.GetUniqueGenresAsync();

        // Assert
        Assert.That(genres, Is.Not.Null);
        Assert.That(genres.Count, Is.EqualTo(5));
        Assert.That(genres[0], Is.EqualTo("Avontuur"));
        Assert.That(genres[1], Is.EqualTo("Fantasy"));
        Assert.That(genres[2], Is.EqualTo("Romantiek"));
        Assert.That(genres[3], Is.EqualTo("Sci-Fi"));
        Assert.That(genres[4], Is.EqualTo("Thriller"));
    }

    [Test]
    public async Task GetAuthorsByGenreAsync_GeeftAuteursVoorGenreGesorteerd()
    {
        // Act
        var auteurs = await _repository.GetAuthorsByGenreAsync("Thriller");

        // Assert
        Assert.That(auteurs, Is.Not.Null);
        Assert.That(auteurs.Count, Is.EqualTo(2));
        Assert.That(auteurs[0], Is.EqualTo("Herman Koch"));
        Assert.That(auteurs[1], Is.EqualTo("Jeroen Olyslagers"));
    }

    [Test]
    public async Task GetAuthorsByGenreAsync_GeeftLegeLijstVoorOnbekendGenre()
    {
        // Act
        var auteurs = await _repository.GetAuthorsByGenreAsync("Horror");

        // Assert
        Assert.That(auteurs, Is.Not.Null);
        Assert.That(auteurs.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetAuthorsByGenreAsync_CaseInsensitiveVoorGenre()
    {
        // Act
        var auteurs = await _repository.GetAuthorsByGenreAsync("thriller");

        // Assert
        Assert.That(auteurs, Is.Not.Null);
        Assert.That(auteurs.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetAveragePriceAsync_GeeftCorrectGemiddelde()
    {
        // Act
        var gemiddelde = await _repository.GetAveragePriceAsync();

        // Assert
        // (18.99 + 14.50 + 19.99 + 22.00 + 16.75 + 20.50 + 15.00) / 7 = 127.73 / 7 = 18.2471...
        Assert.That(gemiddelde, Is.GreaterThan(18.24).And.LessThan(18.25));
    }

    [Test]
    public async Task GetMinPriceAsync_GeeftLaagstePrijs()
    {
        // Act
        var minPrijs = await _repository.GetMinPriceAsync();

        // Assert
        Assert.That(minPrijs, Is.EqualTo(14.50));
    }

    [Test]
    public async Task GetMaxPriceAsync_GeeftHoogstePrijs()
    {
        // Act
        var maxPrijs = await _repository.GetMaxPriceAsync();

        // Assert
        Assert.That(maxPrijs, Is.EqualTo(22.00));
    }

    [Test]
    public async Task GetBooksByAuthorAsync_GeeftBoekenVoorAuteurGesorteerdOpPrijs()
    {
        // Act
        var boeken = await _repository.GetBooksByAuthorAsync("J.K. Rowling");

        // Assert
        Assert.That(boeken, Is.Not.Null);
        Assert.That(boeken.Count, Is.EqualTo(1));
        Assert.That(boeken[0].Titel, Is.EqualTo("Harry Potter en de Steen der Wijzen"));
        Assert.That(boeken[0].Prijs, Is.EqualTo(19.99));
    }

    [Test]
    public async Task GetBooksByAuthorAsync_CaseInsensitiveVoorAuteur()
    {
        // Act
        var boeken = await _repository.GetBooksByAuthorAsync("j.k. rowling");

        // Assert
        Assert.That(boeken, Is.Not.Null);
        Assert.That(boeken.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task GetBooksByAuthorAsync_GeeftLegeLijstVoorOnbekendeAuteur()
    {
        // Act
        var boeken = await _repository.GetBooksByAuthorAsync("Onbekende auteur");

        // Assert
        Assert.That(boeken, Is.Not.Null);
        Assert.That(boeken.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetBooksWithVoorraadGreaterThanAsync_GeeftBoekenMetVoorraadGroterDanMinVoorraad()
    {
        // Act
        var boeken = await _repository.GetBooksWithVoorraadGreaterThanAsync(10);

        // Assert
        Assert.That(boeken, Is.Not.Null);
        Assert.That(boeken.Count, Is.EqualTo(3));
        Assert.That(boeken[0].Prijs, Is.EqualTo(18.99).Within(0.001));
        Assert.That(boeken[1].Prijs, Is.EqualTo(19.99).Within(0.001));
        Assert.That(boeken[2].Prijs, Is.EqualTo(22.00).Within(0.001));
    }

    [Test]
    public async Task GetBooksWithVoorraadGreaterThanAsync_GeeftLegeLijstBijHogeMinVoorraad()
    {
        // Act
        var boeken = await _repository.GetBooksWithVoorraadGreaterThanAsync(100);

        // Assert
        Assert.That(boeken, Is.Not.Null);
        Assert.That(boeken.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task HasAnyBookInGenreAsync_GeeftTrueWanneerGenreBestaat()
    {
        // Act
        var exists = await _repository.HasAnyBookInGenreAsync("Fantasy");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task HasAnyBookInGenreAsync_GeeftFalseWanneerGenreNietBestaat()
    {
        // Act
        var exists = await _repository.HasAnyBookInGenreAsync("Horror");

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task HasAnyBookInGenreAsync_CaseInsensitiveVoorGenre()
    {
        // Act
        var exists = await _repository.HasAnyBookInGenreAsync("fantasy");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task GetPublisherNamesAsync_GeeftUniekeUitgeverijenGesorteerd()
    {
        // Act
        var uitgeverijen = await _repository.GetPublisherNamesAsync();

        // Assert
        Assert.That(uitgeverijen, Is.Not.Null);
        Assert.That(uitgeverijen.Count, Is.EqualTo(5));
        Assert.That(uitgeverijen[0], Is.EqualTo("Ambo|Anthos"));
        Assert.That(uitgeverijen[1], Is.EqualTo("Lannoo"));
        Assert.That(uitgeverijen[2], Is.EqualTo("Uitgeverij Contact"));
        Assert.That(uitgeverijen[3], Is.EqualTo("Uitgeverij Lannoo"));
        Assert.That(uitgeverijen[4], Is.EqualTo("Uitgeverij Prometheus"));
    }
}
