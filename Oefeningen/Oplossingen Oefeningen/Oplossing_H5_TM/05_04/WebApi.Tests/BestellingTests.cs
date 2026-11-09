using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Controllers;
using WebApi.Data;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests;

public class BestellingEndpointsTests
{
    private WebApplicationFactory<BestellingController> _factory = null!;
    private HttpClient _client = null!;
    private BestellingContext _context = null!;

    [SetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<BestellingController>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove existing DbContext registration
                    var dbContextDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<BestellingContext>));
                    if (dbContextDescriptor != null)
                    {
                        services.Remove(dbContextDescriptor);
                    }

                    // Add SQLite in-memory DbContext
                    var sqliteConnection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:");
                    sqliteConnection.Open();

                    services.AddDbContext<BestellingContext>(options =>
                        options.UseSqlite(sqliteConnection));

                    // Replace repository with one using our context
                    var repoDescriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IBestellingRepository));
                    if (repoDescriptor != null)
                    {
                        services.Remove(repoDescriptor);
                    }
                    services.AddScoped<IBestellingRepository, BestellingRepository>();
                });
            });

        _client = _factory.CreateClient();

        // Get the context and apply schema, then remove auto-applied seed data
        using var scope = _factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<BestellingContext>();
        _context.Database.EnsureCreated();

        // Remove auto-applied seed data so we can insert our own
        _context.Bestellingen.RemoveRange(_context.Bestellingen);
        _context.SaveChanges();

        // Reset auto-increment counter in SQLite
        _context.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Bestellingen'");

        // Seed data
        _context.Bestellingen.AddRange(
            new Bestelling { Id = 1, KlantNaam = "Jansen", Tafelnummer = 5, Gerechten = "Lasagne-Salade", Status = "Klaar", TotaalPrijs = 24.50 },
            new Bestelling { Id = 2, KlantNaam = "De Smet", Tafelnummer = 12, Gerechten = "Risotto-Wijn", Status = "In bereiding", TotaalPrijs = 32.00 },
            new Bestelling { Id = 3, KlantNaam = "Peeters", Tafelnummer = 3, Gerechten = "Carpaccio-Koffie", Status = "Afgehaald", TotaalPrijs = 18.75 },
            new Bestelling { Id = 4, KlantNaam = "Willems", Tafelnummer = 8, Gerechten = "Gegrilde Kabeljauw-Dessert", Status = "Betaald", TotaalPrijs = 45.00 },
            new Bestelling { Id = 5, KlantNaam = "Claes", Tafelnummer = 1, Gerechten = "Stoofvlees-Bier", Status = "In bereiding", TotaalPrijs = 28.50 },
            new Bestelling { Id = 6, KlantNaam = "Dubois", Tafelnummer = 15, Gerechten = "Moules-Frites", Status = "Klaar", TotaalPrijs = 22.00 }
        );
        _context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
        _context?.Dispose();
    }

    // --- CRUD-methodes ---

    [Test]
    public async Task GetAllBestellingen_GeeftAlleBestellingenMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetAllBestellingen_GeeftZesSeedBestellingenGesorteerdOpTafelnummer()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(6));
        Assert.That(bestellingen[0].Tafelnummer, Is.EqualTo(1));
        Assert.That(bestellingen[1].Tafelnummer, Is.EqualTo(3));
        Assert.That(bestellingen[2].Tafelnummer, Is.EqualTo(5));
        Assert.That(bestellingen[3].Tafelnummer, Is.EqualTo(8));
        Assert.That(bestellingen[4].Tafelnummer, Is.EqualTo(12));
        Assert.That(bestellingen[5].Tafelnummer, Is.EqualTo(15));
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public async Task GetBestelling_MetId_GeeftCorrecteBestellingMetStatusOk(int id)
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync($"/bestellingen/{id}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [TestCase(1, "Jansen", 5, "Lasagne-Salade", "Klaar", 24.50)]
    [TestCase(3, "Peeters", 3, "Carpaccio-Koffie", "Afgehaald", 18.75)]
    [TestCase(5, "Claes", 1, "Stoofvlees-Bier", "In bereiding", 28.50)]
    public async Task GetBestelling_MetId_GeeftCorrecteInhoud(int id, string klantNaam, int tafelnummer, string gerechten, string status, double totaalPrijs)
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync($"/bestellingen/{id}");
        var bestelling = await response.Content.ReadFromJsonAsync<Bestelling>();

        // Assert
        Assert.That(bestelling, Is.Not.Null);
        Assert.That(bestelling!.Id, Is.EqualTo(id));
        Assert.That(bestelling.KlantNaam, Is.EqualTo(klantNaam));
        Assert.That(bestelling.Tafelnummer, Is.EqualTo(tafelnummer));
        Assert.That(bestelling.Gerechten, Is.EqualTo(gerechten));
        Assert.That(bestelling.Status, Is.EqualTo(status));
        Assert.That(bestelling.TotaalPrijs, Is.EqualTo(totaalPrijs));
    }

    [Test]
    public async Task GetBestelling_OnbestaandeId_GeeftNotFound()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/99");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [TestCase(7)]
    [TestCase(50)]
    [TestCase(100)]
    public async Task GetBestelling_VerschillendeOnbestaandeIds_GeeftNotFound(int id)
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync($"/bestellingen/{id}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task CreateBestelling_GeeftCreatedMetStatus201()
    {
        // Arrange
        var nieuweBestelling = new Bestelling { KlantNaam = "De Vries", Tafelnummer = 7, Gerechten = "Pasta-Salade", Status = "In bereiding", TotaalPrijs = 15.50 };

        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/bestellingen", nieuweBestelling);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
    }

    [Test]
    public async Task CreateBestelling_GeeftNieuweBestellingTerug()
    {
        // Arrange
        var nieuweBestelling = new Bestelling { KlantNaam = "De Vries", Tafelnummer = 7, Gerechten = "Pasta-Salade", Status = "In bereiding", TotaalPrijs = 15.50 };

        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/bestellingen", nieuweBestelling);
        var result = await response.Content.ReadFromJsonAsync<Bestelling>();

        // Assert
        Assert.That(result!.Id, Is.EqualTo(7));
        Assert.That(result.KlantNaam, Is.EqualTo("De Vries"));
        Assert.That(result.Tafelnummer, Is.EqualTo(7));
        Assert.That(result.Gerechten, Is.EqualTo("Pasta-Salade"));
        Assert.That(result.Status, Is.EqualTo("In bereiding"));
        Assert.That(result.TotaalPrijs, Is.EqualTo(15.50));
    }

    [Test]
    public async Task CreateBestelling_DoorCreerenWordtBestellingOpHaalbaarViaGet()
    {
        // Arrange
        var nieuweBestelling = new Bestelling { KlantNaam = "De Vries", Tafelnummer = 7, Gerechten = "Pasta-Salade", Status = "In bereiding", TotaalPrijs = 15.50 };

        // Act
        await _client.PostAsJsonAsync("/bestellingen", nieuweBestelling);

        // Assert - Read after create
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/7");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var opgehaaldeBestelling = await response.Content.ReadFromJsonAsync<Bestelling>();
        Assert.That(opgehaaldeBestelling, Is.Not.Null);
        Assert.That(opgehaaldeBestelling!.KlantNaam, Is.EqualTo("De Vries"));
    }

    [Test]
    public async Task UpdateBestelling_GeeftNoContentMetStatus204()
    {
        // Arrange
        var gewijzigdeBestelling = new Bestelling { KlantNaam = "Jansen", Tafelnummer = 5, Gerechten = "Lasagne-Wijn", Status = "Klaar", TotaalPrijs = 28.00 };

        // Act
        HttpResponseMessage response = await _client.PutAsJsonAsync("/bestellingen/1", gewijzigdeBestelling);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task UpdateBestelling_WerktBestellingBijViaGet()
    {
        // Arrange
        var gewijzigdeBestelling = new Bestelling { KlantNaam = "Jansen", Tafelnummer = 5, Gerechten = "Lasagne-Wijn", Status = "Klaar", TotaalPrijs = 28.00 };

        // Act
        await _client.PutAsJsonAsync("/bestellingen/1", gewijzigdeBestelling);

        // Assert - Read after update
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/1");
        var opgehaaldeBestelling = await response.Content.ReadFromJsonAsync<Bestelling>();
        Assert.That(opgehaaldeBestelling, Is.Not.Null);
        Assert.That(opgehaaldeBestelling!.Gerechten, Is.EqualTo("Lasagne-Wijn"));
        Assert.That(opgehaaldeBestelling.TotaalPrijs, Is.EqualTo(28.00));
    }

    [Test]
    public async Task UpdateBestelling_IdWordtNietOverschrevenDoorRequestBody()
    {
        // Arrange
        var gewijzigdeBestelling = new Bestelling { Id = 99, KlantNaam = "Jansen", Tafelnummer = 5, Gerechten = "Lasagne-Wijn", Status = "Klaar", TotaalPrijs = 28.00 };

        // Act
        await _client.PutAsJsonAsync("/bestellingen/1", gewijzigdeBestelling);

        // Assert - Read after update
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/1");
        var opgehaaldeBestelling = await response.Content.ReadFromJsonAsync<Bestelling>();
        Assert.That(opgehaaldeBestelling, Is.Not.Null);
        Assert.That(opgehaaldeBestelling!.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task UpdateBestelling_OnbestaandeId_GeeftNotFound()
    {
        // Arrange
        var gewijzigdeBestelling = new Bestelling { KlantNaam = "Jansen", Tafelnummer = 5, Gerechten = "Lasagne-Wijn", Status = "Klaar", TotaalPrijs = 28.00 };

        // Act
        HttpResponseMessage response = await _client.PutAsJsonAsync("/bestellingen/99", gewijzigdeBestelling);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteBestelling_GeeftNoContentMetStatus204()
    {
        // Act
        HttpResponseMessage response = await _client.DeleteAsync("/bestellingen/1");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }

    [Test]
    public async Task DeleteBestelling_WordtBestellingVerwijderdViaGet()
    {
        // Act
        await _client.DeleteAsync("/bestellingen/1");

        // Assert - Read after delete
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/1");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task DeleteBestelling_OnbestaandeId_GeeftNotFound()
    {
        // Act
        HttpResponseMessage response = await _client.DeleteAsync("/bestellingen/99");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    // --- Query-methodes via endpoints ---

    [Test]
    public async Task GetByStatus_GeeftBestellingenMetGevraagdeStatusMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/status/Klaar");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetByStatus_GeeftCorrecteBestellingenGesorteerdOpTafelnummer()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/status/Klaar");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(2));
        Assert.That(bestellingen[0].Tafelnummer, Is.EqualTo(5));
        Assert.That(bestellingen[1].Tafelnummer, Is.EqualTo(15));
    }

    [Test]
    public async Task GetByStatus_CaseInsensitiveVoorStatus()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/status/klaar");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetByStatus_GeeftLegeLijstVoorOnbekendeStatusMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/status/Onbekend");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetByTafel_GeeftBestellingenVoorTafelMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/tafel/5");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task GetByTafel_GeeftCorrecteBestellingGesorteerdOpTotaalPrijs()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/tafel/5");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(1));
        Assert.That(bestellingen[0].KlantNaam, Is.EqualTo("Jansen"));
        Assert.That(bestellingen[0].TotaalPrijs, Is.EqualTo(24.50));
    }

    [Test]
    public async Task GetByTafel_GeeftLegeLijstVoorOnbekendeTafelMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/tafel/99");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetSortedByTotaalPrijs_OplopendWanneerDescendingFalse()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/sorteren/oprijs?descending=false");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(6));
        Assert.That(bestellingen[0].TotaalPrijs, Is.EqualTo(18.75));
        Assert.That(bestellingen[1].TotaalPrijs, Is.EqualTo(22.00));
        Assert.That(bestellingen[2].TotaalPrijs, Is.EqualTo(24.50));
        Assert.That(bestellingen[3].TotaalPrijs, Is.EqualTo(28.50));
        Assert.That(bestellingen[4].TotaalPrijs, Is.EqualTo(32.00));
        Assert.That(bestellingen[5].TotaalPrijs, Is.EqualTo(45.00));
    }

    [Test]
    public async Task GetSortedByTotaalPrijs_AflopendWanneerDescendingTrue()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/sorteren/oprijs?descending=true");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(6));
        Assert.That(bestellingen[0].TotaalPrijs, Is.EqualTo(45.00));
        Assert.That(bestellingen[1].TotaalPrijs, Is.EqualTo(32.00));
        Assert.That(bestellingen[2].TotaalPrijs, Is.EqualTo(28.50));
        Assert.That(bestellingen[3].TotaalPrijs, Is.EqualTo(24.50));
        Assert.That(bestellingen[4].TotaalPrijs, Is.EqualTo(22.00));
        Assert.That(bestellingen[5].TotaalPrijs, Is.EqualTo(18.75));
    }

    [Test]
    public async Task GetSortedByTotaalPrijs_OplopendWanneerDescendingNietOpgegeven()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/sorteren/oprijs");
        var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

        // Assert
        Assert.That(bestellingen, Is.Not.Null);
        Assert.That(bestellingen!.Count, Is.EqualTo(6));
        Assert.That(bestellingen[0].TotaalPrijs, Is.EqualTo(18.75));
    }

    [Test]
    public async Task GetOmzet_GeeftTotaleOmzetMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/omzet");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("totalRevenue").GetDouble(), Is.EqualTo(170.75));
    }

    [Test]
    public async Task GetGemiddelde_GeeftGemiddeldeBestelwaardeMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/gemiddelde");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        // (24.50 + 32.00 + 18.75 + 45.00 + 28.50 + 22.00) / 6 = 170.75 / 6 = 28.4583...
        Assert.That(result.GetProperty("averageOrderValue").GetDouble(), Is.GreaterThan(28.45).And.LessThan(28.46));
    }

    [Test]
    public async Task GetCountByStatus_GeeftCorrectAantalMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/tellen/status/Klaar");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("count").GetInt32(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetCountByStatus_CaseInsensitiveVoorStatus()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/tellen/status/klaar");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("count").GetInt32(), Is.EqualTo(2));
    }

    [Test]
    public async Task GetCountByStatus_GeeftNulVoorOnbekendeStatusMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/tellen/status/Onbekend");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("count").GetInt32(), Is.EqualTo(0));
    }

    [Test]
    public async Task GetExistsByStatus_GeeftTrueWanneerStatusBestaatMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/exists/status/Klaar");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("exists").GetBoolean(), Is.True);
    }

    [Test]
    public async Task GetExistsByStatus_GeeftFalseWanneerStatusNietBestaatMetStatusOk()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/exists/status/Onbekend");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("exists").GetBoolean(), Is.False);
    }

    [Test]
    public async Task GetExistsByStatus_CaseInsensitiveVoorStatus()
    {
        // Act
        HttpResponseMessage response = await _client.GetAsync("/bestellingen/exists/status/klaar");
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var result = doc.RootElement;

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(result.GetProperty("exists").GetBoolean(), Is.True);
    }
}
