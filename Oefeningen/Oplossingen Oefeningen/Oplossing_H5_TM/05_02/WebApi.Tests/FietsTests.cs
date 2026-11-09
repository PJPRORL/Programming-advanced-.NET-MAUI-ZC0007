using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests;

public class FietsEndpointsTests
{
    private SqliteConnection _connection = null!;
    private FietsCatalogusContext _context = null!;
    private IFietsRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<FietsCatalogusContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new FietsCatalogusContext(options);
        _context.Database.EnsureCreated();

        // Remove seed data so tests start with empty database
        _context.Fietsen.RemoveRange(_context.Fietsen);
        _context.SaveChanges();

        // Reset auto-increment counter in SQLite
        _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Fietsen'");

        _repository = new FietsRepository(_context);
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
    public async Task GetAllAsync_GeeftAlleFietsenGesorteerdOpMerk()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Gazelle", Type = "Stadsfiets", Kleur = "Blauw", Prijs = 800.00, Aantal = 12 });

        // Act
        var fietsen = await _repository.GetAllAsync();

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(3));
        Assert.That(fietsen[0].Merk, Is.EqualTo("Gazelle"));
        Assert.That(fietsen[1].Merk, Is.EqualTo("Specialized"));
        Assert.That(fietsen[2].Merk, Is.EqualTo("Trek"));
    }

    [Test]
    public async Task GetAllAsync_GeeftLegeLijstBijLegeDatabase()
    {
        // Act
        var fietsen = await _repository.GetAllAsync();

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(0));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetByIdAsync_GeeftCorrecteFiets(int id)
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Gazelle", Type = "Stadsfiets", Kleur = "Blauw", Prijs = 800.00, Aantal = 12 });

        // Act
        var fiets = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(fiets, Is.Not.Null);
        Assert.That(fiets!.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task GetByIdAsync_GeeftNullVoorOnbestaandeFiets()
    {
        // Act
        var fiets = await _repository.GetByIdAsync(99);

        // Assert
        Assert.That(fiets, Is.Null);
    }

    [Test]
    public async Task CreateAsync_GeeftNieuweFietsMetToegewezenId()
    {
        // Arrange
        var nieuweFiets = new Fiets { Merk = "Canyon", Type = "Racefiets", Kleur = "Blauw", Prijs = 3200.00, Aantal = 2 };

        // Act
        var result = await _repository.CreateAsync(nieuweFiets);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Merk, Is.EqualTo("Canyon"));
        Assert.That(result.Type, Is.EqualTo("Racefiets"));
        Assert.That(result.Kleur, Is.EqualTo("Blauw"));
        Assert.That(result.Prijs, Is.EqualTo(3200.00));
        Assert.That(result.Aantal, Is.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_WerktBestaandeFietsBij()
    {
        // Arrange
        var fiets = new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 };
        await _repository.CreateAsync(fiets);

        var gewijzigdeFiets = new Fiets { Merk = "Trek", Type = "Racefiets", Kleur = "Rood", Prijs = 1800.00, Aantal = 3 };

        // Act
        await _repository.UpdateAsync(1, gewijzigdeFiets);

        // Assert
        var opgehaaldeFiets = await _repository.GetByIdAsync(1);
        Assert.That(opgehaaldeFiets, Is.Not.Null);
        Assert.That(opgehaaldeFiets!.Type, Is.EqualTo("Racefiets"));
        Assert.That(opgehaaldeFiets.Kleur, Is.EqualTo("Rood"));
        Assert.That(opgehaaldeFiets.Prijs, Is.EqualTo(1800.00));
        Assert.That(opgehaaldeFiets.Aantal, Is.EqualTo(3));
    }

    [Test]
    public async Task UpdateAsync_DoeNietsBijOnbestaandeFiets()
    {
        // Arrange
        var fiets = new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 };
        await _repository.CreateAsync(fiets);

        var gewijzigdeFiets = new Fiets { Merk = "Trek", Type = "Racefiets", Kleur = "Rood", Prijs = 1800.00, Aantal = 3 };

        // Act
        await _repository.UpdateAsync(99, gewijzigdeFiets);

        // Assert
        var opgehaaldeFiets = await _repository.GetByIdAsync(1);
        Assert.That(opgehaaldeFiets, Is.Not.Null);
        Assert.That(opgehaaldeFiets!.Type, Is.EqualTo("Mountainbike"));
    }

    [Test]
    public async Task DeleteAsync_VerwijdertBestaandeFiets()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });

        // Act
        await _repository.DeleteAsync(1);

        // Assert
        var fietsen = await _repository.GetAllAsync();
        Assert.That(fietsen.Count, Is.EqualTo(1));
        Assert.That(fietsen[0].Merk, Is.EqualTo("Specialized"));

        var verwijderdeFiets = await _repository.GetByIdAsync(1);
        Assert.That(verwijderdeFiets, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_DoeNietsBijOnbestaandeFiets()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        await _repository.DeleteAsync(99);

        // Assert
        var fietsen = await _repository.GetAllAsync();
        Assert.That(fietsen.Count, Is.EqualTo(1));
    }

    // --- Query-methodes ---

    [Test]
    public async Task GetByMerkAsync_GeeftFietsenMetGevraagdMerkGesorteerdOpPrijs()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1800.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Racefiets", Kleur = "Groen", Prijs = 1200.00, Aantal = 5 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });

        // Act
        var fietsen = await _repository.GetByMerkAsync("Trek");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(2));
        Assert.That(fietsen[0].Prijs, Is.EqualTo(1200.00));
        Assert.That(fietsen[1].Prijs, Is.EqualTo(1800.00));
    }

    [Test]
    public async Task GetByMerkAsync_GeeftLegeLijstBijOnbekendMerk()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var fietsen = await _repository.GetByMerkAsync("Giant");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetByMerkAsync_CaseInsensitiveVoorMerk()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var fietsen = await _repository.GetByMerkAsync("trek");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(1));
        Assert.That(fietsen[0].Merk, Is.EqualTo("Trek"));
    }

    [Test]
    public async Task GetByTypeAsync_GeeftFietsenMetGevraagdTypeGesorteerdOpPrijs()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Racefiets", Kleur = "Groen", Prijs = 1800.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Gazelle", Type = "Stadsfiets", Kleur = "Blauw", Prijs = 800.00, Aantal = 12 });

        // Act
        var fietsen = await _repository.GetByTypeAsync("Racefiets");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(2));
        Assert.That(fietsen[0].Prijs, Is.EqualTo(1800.00));
        Assert.That(fietsen[1].Prijs, Is.EqualTo(2500.00));
    }

    [Test]
    public async Task GetByTypeAsync_GeeftLegeLijstBijOnbekendType()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var fietsen = await _repository.GetByTypeAsync("Gravel");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetByKleurAsync_GeeftFietsenMetGevraagdeKleurGesorteerdOpPrijs()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1500.00, Aantal = 7 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Canyon", Type = "Racefiets", Kleur = "Zwart", Prijs = 1800.00, Aantal = 2 });

        // Act
        var fietsen = await _repository.GetByKleurAsync("Zwart");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(2));
        Assert.That(fietsen[0].Prijs, Is.EqualTo(1500.00));
        Assert.That(fietsen[1].Prijs, Is.EqualTo(1800.00));
    }

    [Test]
    public async Task GetByKleurAsync_GeeftLegeLijstBijOnbekendeKleur()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var fietsen = await _repository.GetByKleurAsync("Groen");

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetInStockAsync_GeeftFietsenMetAantalGroterDan0GesorteerdOpPrijs()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1500.00, Aantal = 7 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 0 });
        await _repository.CreateAsync(new Fiets { Merk = "Canyon", Type = "Racefiets", Kleur = "Zwart", Prijs = 800.00, Aantal = 3 });

        // Act
        var fietsen = await _repository.GetInStockAsync();

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(2));
        Assert.That(fietsen[0].Prijs, Is.EqualTo(800.00));
        Assert.That(fietsen[1].Prijs, Is.EqualTo(1500.00));
    }

    [Test]
    public async Task GetInStockAsync_GeeftLegeLijstBijGeenVoorraad()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 0 });

        // Act
        var fietsen = await _repository.GetInStockAsync();

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetSortedByPrijsAsync_GeeftOplopendGesorteerdWanneerDescendingFalse()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1800.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 1200.00, Aantal = 5 });
        await _repository.CreateAsync(new Fiets { Merk = "Canyon", Type = "Racefiets", Kleur = "Blauw", Prijs = 2500.00, Aantal = 2 });

        // Act
        var fietsen = await _repository.GetSortedByPrijsAsync(false);

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(3));
        Assert.That(fietsen[0].Prijs, Is.EqualTo(1200.00));
        Assert.That(fietsen[1].Prijs, Is.EqualTo(1800.00));
        Assert.That(fietsen[2].Prijs, Is.EqualTo(2500.00));
    }

    [Test]
    public async Task GetSortedByPrijsAsync_GeeftAflopendGesorteerdWanneerDescendingTrue()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Canyon", Type = "Racefiets", Kleur = "Blauw", Prijs = 1800.00, Aantal = 2 });

        // Act
        var fietsen = await _repository.GetSortedByPrijsAsync(true);

        // Assert
        Assert.That(fietsen, Is.Not.Null);
        Assert.That(fietsen.Count, Is.EqualTo(3));
        Assert.That(fietsen[0].Prijs, Is.EqualTo(2500.00));
        Assert.That(fietsen[1].Prijs, Is.EqualTo(1800.00));
        Assert.That(fietsen[2].Prijs, Is.EqualTo(1200.00));
    }

    [Test]
    public async Task ExistsByMerkAsync_GeeftTrueWanneerMerkBestaat()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var exists = await _repository.ExistsByMerkAsync("Trek");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task ExistsByMerkAsync_GeeftFalseWanneerMerkNietBestaat()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var exists = await _repository.ExistsByMerkAsync("Canyon");

        // Assert
        Assert.That(exists, Is.False);
    }

    [Test]
    public async Task ExistsByMerkAsync_CaseInsensitiveVoorMerk()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var exists = await _repository.ExistsByMerkAsync("trek");

        // Assert
        Assert.That(exists, Is.True);
    }

    [Test]
    public async Task CountByTypeAsync_GeeftCorrectAantalVoorGevraagdType()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Racefiets", Kleur = "Groen", Prijs = 1800.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Canyon", Type = "Mountainbike", Kleur = "Blauw", Prijs = 1200.00, Aantal = 5 });

        // Act
        var count = await _repository.CountByTypeAsync("Racefiets");

        // Assert
        Assert.That(count, Is.EqualTo(2));
    }

    [Test]
    public async Task CountByTypeAsync_Geeft0WanneerGeenFietsenVoorType()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Mountainbike", Kleur = "Zwart", Prijs = 1200.00, Aantal = 5 });

        // Act
        var count = await _repository.CountByTypeAsync("Gravel");

        // Assert
        Assert.That(count, Is.EqualTo(0));
    }

    [Test]
    public async Task CountByTypeAsync_CaseInsensitiveVoorType()
    {
        // Arrange
        await _repository.CreateAsync(new Fiets { Merk = "Trek", Type = "Racefiets", Kleur = "Groen", Prijs = 1800.00, Aantal = 3 });
        await _repository.CreateAsync(new Fiets { Merk = "Specialized", Type = "Racefiets", Kleur = "Rood", Prijs = 2500.00, Aantal = 3 });

        // Act
        var count = await _repository.CountByTypeAsync("racefiets");

        // Assert
        Assert.That(count, Is.EqualTo(2));
    }
}
