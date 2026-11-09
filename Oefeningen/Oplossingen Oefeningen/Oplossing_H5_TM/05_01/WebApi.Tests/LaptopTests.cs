using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests;

public class LaptopEndpointsTests
{
    private SqliteConnection _connection = null!;
    private LaptopCatalogusContext _context = null!;
    private ILaptopRepository _repository = null!;

    [SetUp]
    public void Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<LaptopCatalogusContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new LaptopCatalogusContext(options);
        _context.Database.EnsureCreated();

        _repository = new LaptopRepository(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }

    [Test]
    public async Task GetAllAsync_GeeftAlleLaptopsGesorteerdOpMerk()
    {
        // Arrange
        await _repository.CreateAsync(new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" });
        await _repository.CreateAsync(new Laptop { Merk = "HP", Processor = "AMD Ryzen 7", RamInGB = 16, Prijs = 1199.99, GPU = "RX 6600" });
        await _repository.CreateAsync(new Laptop { Merk = "Lenovo", Processor = "Intel i7", RamInGB = 32, Prijs = 1499.99, GPU = "RTX 3070" });

        // Act
        var laptops = await _repository.GetAllAsync();

        // Assert
        Assert.That(laptops, Is.Not.Null);
        Assert.That(laptops.Count, Is.EqualTo(3));
        Assert.That(laptops[0].Merk, Is.EqualTo("Dell"));
        Assert.That(laptops[1].Merk, Is.EqualTo("HP"));
        Assert.That(laptops[2].Merk, Is.EqualTo("Lenovo"));
    }

    [Test]
    public async Task GetAllAsync_GeeftGesorteerdeLaptopsBijLegeDatabase()
    {
        // Act
        var laptops = await _repository.GetAllAsync();

        // Assert
        Assert.That(laptops, Is.Not.Null);
        Assert.That(laptops.Count, Is.EqualTo(0));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public async Task GetByIdAsync_GeeftCorrecteLaptop(int id)
    {
        // Arrange
        await _repository.CreateAsync(new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" });
        await _repository.CreateAsync(new Laptop { Merk = "HP", Processor = "AMD Ryzen 7", RamInGB = 16, Prijs = 1199.99, GPU = "RX 6600" });
        await _repository.CreateAsync(new Laptop { Merk = "Lenovo", Processor = "Intel i7", RamInGB = 32, Prijs = 1499.99, GPU = "RTX 3070" });

        // Act
        var laptop = await _repository.GetByIdAsync(id);

        // Assert
        Assert.That(laptop, Is.Not.Null);
        Assert.That(laptop!.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task GetByIdAsync_GeeftNullVoorOnbestaandeLaptop()
    {
        // Act
        var laptop = await _repository.GetByIdAsync(99);

        // Assert
        Assert.That(laptop, Is.Null);
    }

    [Test]
    public async Task CreateAsync_GeeftNieuweLaptopMetToegewezenId()
    {
        // Arrange
        var nieuweLaptop = new Laptop { Merk = "Apple", Processor = "M2 Pro", RamInGB = 16, Prijs = 2499.99, GPU = "M2 Pro GPU" };

        // Act
        var result = await _repository.CreateAsync(nieuweLaptop);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Merk, Is.EqualTo("Apple"));
        Assert.That(result.Processor, Is.EqualTo("M2 Pro"));
        Assert.That(result.RamInGB, Is.EqualTo(16));
        Assert.That(result.Prijs, Is.EqualTo(2499.99));
        Assert.That(result.GPU, Is.EqualTo("M2 Pro GPU"));
    }

    [Test]
    public async Task CreateAsync_GeeftCorrecteVolgendeId()
    {
        // Arrange
        await _repository.CreateAsync(new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" });
        var nieuweLaptop = new Laptop { Merk = "HP", Processor = "AMD Ryzen 7", RamInGB = 16, Prijs = 1199.99, GPU = "RX 6600" };

        // Act
        var result = await _repository.CreateAsync(nieuweLaptop);

        // Assert
        Assert.That(result.Id, Is.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_WerktBestaandeLaptopBij()
    {
        // Arrange
        var laptop = new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" };
        await _repository.CreateAsync(laptop);

        var gewijzigdeLaptop = new Laptop { Merk = "Dell", Processor = "Intel i7", RamInGB = 16, Prijs = 1299.99, GPU = "RTX 3060" };

        // Act
        await _repository.UpdateAsync(1, gewijzigdeLaptop);

        // Assert
        var opgehaaldeLaptop = await _repository.GetByIdAsync(1);
        Assert.That(opgehaaldeLaptop, Is.Not.Null);
        Assert.That(opgehaaldeLaptop!.Processor, Is.EqualTo("Intel i7"));
        Assert.That(opgehaaldeLaptop.RamInGB, Is.EqualTo(16));
        Assert.That(opgehaaldeLaptop.Prijs, Is.EqualTo(1299.99));
        Assert.That(opgehaaldeLaptop.GPU, Is.EqualTo("RTX 3060"));
    }

    [Test]
    public async Task UpdateAsync_DoeNietsBijOnbestaandeLaptop()
    {
        // Arrange
        var laptop = new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" };
        await _repository.CreateAsync(laptop);

        var gewijzigdeLaptop = new Laptop { Merk = "Dell", Processor = "Intel i7", RamInGB = 16, Prijs = 1299.99, GPU = "RTX 3060" };

        // Act
        await _repository.UpdateAsync(99, gewijzigdeLaptop);

        // Assert
        var opgehaaldeLaptop = await _repository.GetByIdAsync(1);
        Assert.That(opgehaaldeLaptop, Is.Not.Null);
        Assert.That(opgehaaldeLaptop!.Processor, Is.EqualTo("Intel i5"));
    }

    [Test]
    public async Task DeleteAsync_VerwijdertBestaandeLaptop()
    {
        // Arrange
        await _repository.CreateAsync(new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" });
        await _repository.CreateAsync(new Laptop { Merk = "HP", Processor = "AMD Ryzen 7", RamInGB = 16, Prijs = 1199.99, GPU = "RX 6600" });

        // Act
        await _repository.DeleteAsync(1);

        // Assert
        var laptops = await _repository.GetAllAsync();
        Assert.That(laptops.Count, Is.EqualTo(1));
        Assert.That(laptops[0].Merk, Is.EqualTo("HP"));

        var verwijderdeLaptop = await _repository.GetByIdAsync(1);
        Assert.That(verwijderdeLaptop, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_DoeNietsBijOnbestaandeLaptop()
    {
        // Arrange
        await _repository.CreateAsync(new Laptop { Merk = "Dell", Processor = "Intel i5", RamInGB = 8, Prijs = 899.99, GPU = "MX150" });

        // Act
        await _repository.DeleteAsync(99);

        // Assert
        var laptops = await _repository.GetAllAsync();
        Assert.That(laptops.Count, Is.EqualTo(1));
    }
}
