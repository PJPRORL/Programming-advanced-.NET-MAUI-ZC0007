using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests
{
    public class BestellingEndpointsTests
    {
        private WebApplicationFactory<BestellingController> _factory = null!;
        private HttpClient _client = null!;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<BestellingController>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(IBestellingRepository));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddSingleton<IBestellingRepository, InMemoryBestellingRepository>();
                    });
                });
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetBestellingen_GeeftAlleBestellingenMetStatusOk()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/bestellingen");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetBestellingen_GeeftDrieSeedBestellingenTerug()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/bestellingen");
            var bestellingen = await response.Content.ReadFromJsonAsync<List<Bestelling>>();

            // Assert
            Assert.That(bestellingen, Is.Not.Null);
            Assert.That(bestellingen!.Count, Is.EqualTo(3));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetBestelling_MetId_GeeftCorrectBestellingMetStatusOk(int id)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/bestellingen/{id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TestCase(1, "Anna Jansen", 4, "Lasagne-Salade", "Gereed")]
        [TestCase(2, "Youssef Benali", 7, "Risotto-Gegrilde Groenten", "Bereiden")]
        [TestCase(3, "Maria De Smet", 2, "Pasta Carbonara", "Gereed")]
        public async Task GetBestelling_MetId_GeeftCorrecteInhoud(int id, string naam, int tafelnummer, string gerechten, string status)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/bestellingen/{id}");
            var bestelling = await response.Content.ReadFromJsonAsync<Bestelling>();

            // Assert
            Assert.That(bestelling, Is.Not.Null);
            Assert.That(bestelling!.Id, Is.EqualTo(id));
            Assert.That(bestelling.Naam, Is.EqualTo(naam));
            Assert.That(bestelling.Tafelnummer, Is.EqualTo(tafelnummer));
            Assert.That(bestelling.Gerechten, Is.EqualTo(gerechten));
            Assert.That(bestelling.Status, Is.EqualTo(status));
        }

        [Test]
        public async Task GetBestelling_OnbestaandeId_GeeftNotFound()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/bestellingen/99");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestCase(4)]
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
        public async Task PostBestelling_GeeftCreatedMetStatus201()
        {
            // Arrange
            var body = new Bestelling { Id = 0, Naam = "Jan Peeters", Tafelnummer = 5, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/bestellingen", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task PostBestelling_GeeftTerugHetGemaakteBestelling()
        {
            // Arrange
            var body = new Bestelling { Id = 0, Naam = "Jan Peeters", Tafelnummer = 5, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/bestellingen", content);
            var bestelling = await response.Content.ReadFromJsonAsync<Bestelling>();

            // Assert
            Assert.That(bestelling, Is.Not.Null);
            Assert.That(bestelling!.Naam, Is.EqualTo("Jan Peeters"));
            Assert.That(bestelling.Tafelnummer, Is.EqualTo(5));
            Assert.That(bestelling.Gerechten, Is.EqualTo("Soep-Salade"));
            Assert.That(bestelling.Status, Is.EqualTo("InWachtrij"));
        }

        [Test]
        public async Task PostBestelling_BerekentIdAlsMaxPlusEen()
        {
            // Arrange - get current max ID first
            HttpResponseMessage getResponse = await _client.GetAsync("/bestellingen");
            var bestellingen = await getResponse.Content.ReadFromJsonAsync<List<Bestelling>>();

            int maxId = bestellingen!.Max(b => b.Id);

            var body = new Bestelling { Id = 0, Naam = "Jan Peeters", Tafelnummer = 5, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/bestellingen", content);
            var bestelling = await response.Content.ReadFromJsonAsync<Bestelling>();

            // Assert
            Assert.That(bestelling!.Id, Is.EqualTo(maxId + 1));
        }

        [Test]
        public async Task PostBestelling_IgnoreertIdUitRequestBody()
        {
            // Arrange - send a bestelling with id=999 which should be ignored
            var body = new Bestelling { Id = 999, Naam = "Jan Peeters", Tafelnummer = 5, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/bestellingen", content);
            var bestelling = await response.Content.ReadFromJsonAsync<Bestelling>();

            // Assert - the returned ID should not be 999
            Assert.That(bestelling!.Id, Is.Not.EqualTo(999));
        }

        [Test]
        public async Task PostBestelling_NaCreate_GeeftBestellingTerugViaGet()
        {
            // Arrange
            var body = new Bestelling { Id = 0, Naam = "Jan Peeters", Tafelnummer = 5, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act - create the bestelling
            HttpResponseMessage postResponse = await _client.PostAsync("/bestellingen", content);
            var bestelling = await postResponse.Content.ReadFromJsonAsync<Bestelling>();

            // Act - get the bestelling by its returned ID
            int createdId = bestelling!.Id;
            HttpResponseMessage getResponse = await _client.GetAsync($"/bestellingen/{createdId}");
            var retrievedBestelling = await getResponse.Content.ReadFromJsonAsync<Bestelling>();

            // Assert
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(retrievedBestelling!.Id, Is.EqualTo(createdId));
            Assert.That(retrievedBestelling.Naam, Is.EqualTo("Jan Peeters"));
            Assert.That(retrievedBestelling.Tafelnummer, Is.EqualTo(5));
            Assert.That(retrievedBestelling.Gerechten, Is.EqualTo("Soep-Salade"));
            Assert.That(retrievedBestelling.Status, Is.EqualTo("InWachtrij"));
        }

        [Test]
        public async Task PostBestelling_NaCreate_VerschenenInCollectie()
        {
            // Arrange
            var body = new Bestelling { Id = 0, Naam = "Jan Peeters", Tafelnummer = 5, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act - get count before
            HttpResponseMessage getBeforeResponse = await _client.GetAsync("/bestellingen");
            var bestellingenBefore = await getBeforeResponse.Content.ReadFromJsonAsync<List<Bestelling>>();

            // Act - create the bestelling
            HttpResponseMessage postResponse = await _client.PostAsync("/bestellingen", content);

            // Act - get count after
            HttpResponseMessage getAfterResponse = await _client.GetAsync("/bestellingen");
            var bestellingenAfter = await getAfterResponse.Content.ReadFromJsonAsync<List<Bestelling>>();

            // Assert
            Assert.That(bestellingenAfter!.Count, Is.EqualTo(bestellingenBefore!.Count + 1));
        }

        [Test]
        public async Task PutBestelling_BijwerktAllePropertiesEnGeeftNoContent()
        {
            // Arrange - first create a new bestelling for PUT testing
            var createBody = new Bestelling { Id = 0, Naam = "Originele Bestelling", Tafelnummer = 1, Gerechten = "Oorspronkelijk", Status = "InWachtrij" };
            var createContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createBody), System.Text.Encoding.UTF8);
            createContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createResponse = await _client.PostAsync("/bestellingen", createContent);
            var createdBestelling = await createResponse.Content.ReadFromJsonAsync<Bestelling>();
            int bestellingId = createdBestelling!.Id;

            // Arrange - send updated data
            var body = new Bestelling { Id = bestellingId, Naam = "Bijgewerkte Bestelling", Tafelnummer = 9, Gerechten = "Nieuw Gerecht", Status = "Gereed" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage putResponse = await _client.PutAsync($"/bestellingen/{bestellingId}", content);

            // Assert - immediate response
            Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            // Assert - follow-up GET shows updated values
            HttpResponseMessage getResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");
            var bijgewerkteBestelling = await getResponse.Content.ReadFromJsonAsync<Bestelling>();

            Assert.That(bijgewerkteBestelling!.Id, Is.EqualTo(bestellingId));
            Assert.That(bijgewerkteBestelling.Naam, Is.EqualTo("Bijgewerkte Bestelling"));
            Assert.That(bijgewerkteBestelling.Tafelnummer, Is.EqualTo(9));
            Assert.That(bijgewerkteBestelling.Gerechten, Is.EqualTo("Nieuw Gerecht"));
            Assert.That(bijgewerkteBestelling.Status, Is.EqualTo("Gereed"));
        }

        [Test]
        public async Task PutBestelling_WijzigtMeerderePropertiesTegelijk()
        {
            // Arrange - first create a new bestelling for PUT testing
            var createBody = new Bestelling { Id = 0, Naam = "Test Bestelling", Tafelnummer = 1, Gerechten = "Test", Status = "InWachtrij" };
            var createContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createBody), System.Text.Encoding.UTF8);
            createContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createResponse = await _client.PostAsync("/bestellingen", createContent);
            var createdBestelling = await createResponse.Content.ReadFromJsonAsync<Bestelling>();
            int bestellingId = createdBestelling!.Id;

            // Arrange - send data where all properties change
            var body = new Bestelling { Id = bestellingId, Naam = "Andere Naam", Tafelnummer = 10, Gerechten = "Andere Gerechten", Status = "Bezorgd" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage putResponse = await _client.PutAsync($"/bestellingen/{bestellingId}", content);

            // Assert
            Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            // Verify all changed properties via GET
            HttpResponseMessage getResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");
            var bestelling = await getResponse.Content.ReadFromJsonAsync<Bestelling>();

            Assert.That(bestelling!.Id, Is.EqualTo(bestellingId));
            Assert.That(bestelling.Naam, Is.EqualTo("Andere Naam"));
            Assert.That(bestelling.Tafelnummer, Is.EqualTo(10));
            Assert.That(bestelling.Gerechten, Is.EqualTo("Andere Gerechten"));
            Assert.That(bestelling.Status, Is.EqualTo("Bezorgd"));
        }

        [Test]
        public async Task PutBestelling_OnbestaandeId_GeeftNotFound()
        {
            // Arrange
            var body = new Bestelling { Id = 99, Naam = "Onbekend", Tafelnummer = 1, Gerechten = "Onbekend", Status = "Onbekend" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage putResponse = await _client.PutAsync("/bestellingen/99", content);

            // Assert
            Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestCase(4)]
        [TestCase(50)]
        [TestCase(100)]
        public async Task PutBestelling_VerschillendeOnbestaandeIds_GeeftNotFound(int id)
        {
            // Arrange
            var body = new Bestelling { Id = id, Naam = "Onbekend", Tafelnummer = 1, Gerechten = "Onbekend", Status = "Onbekend" };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage putResponse = await _client.PutAsync($"/bestellingen/{id}", content);

            // Assert
            Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task DeleteBestelling_VerwijdertBestellingEnGeeftNoContent()
        {
            // Arrange - first create a new bestelling for DELETE testing
            var createBody = new Bestelling { Id = 0, Naam = "Te Verwijderen Bestelling", Tafelnummer = 3, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var createContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createBody), System.Text.Encoding.UTF8);
            createContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createResponse = await _client.PostAsync("/bestellingen", createContent);
            var createdBestelling = await createResponse.Content.ReadFromJsonAsync<Bestelling>();
            int bestellingId = createdBestelling!.Id;

            // Arrange - verify the bestelling exists before deletion
            HttpResponseMessage getBeforeResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");
            Assert.That(getBeforeResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            // Act
            HttpResponseMessage deleteResponse = await _client.DeleteAsync($"/bestellingen/{bestellingId}");

            // Assert - immediate response
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            // Assert - follow-up GET returns NotFound
            HttpResponseMessage getAfterResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");
            Assert.That(getAfterResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task DeleteBestelling_NaDelete_GeeftNotFoundVoorCollection()
        {
            // Arrange - first create a new bestelling for DELETE testing
            var createBody = new Bestelling { Id = 0, Naam = "Te Verwijderen Bestelling", Tafelnummer = 3, Gerechten = "Soep-Salade", Status = "InWachtrij" };
            var createContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createBody), System.Text.Encoding.UTF8);
            createContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage createResponse = await _client.PostAsync("/bestellingen", createContent);
            var createdBestelling = await createResponse.Content.ReadFromJsonAsync<Bestelling>();
            int bestellingId = createdBestelling!.Id;

            // Arrange - verify the bestelling exists in the collection before deletion
            HttpResponseMessage getBeforeResponse = await _client.GetAsync("/bestellingen");
            var bestellingenBefore = await getBeforeResponse.Content.ReadFromJsonAsync<List<Bestelling>>();
            var bestaatVoor = bestellingenBefore!.Any(b => b.Id == bestellingId);
            Assert.That(bestaatVoor, Is.True);

            // Act
            HttpResponseMessage deleteResponse = await _client.DeleteAsync($"/bestellingen/{bestellingId}");

            // Act - check the bestelling is no longer in the collection
            HttpResponseMessage getAfterResponse = await _client.GetAsync("/bestellingen");
            var bestellingenAfter = await getAfterResponse.Content.ReadFromJsonAsync<List<Bestelling>>();

            // Assert
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(bestellingenAfter!.Any(b => b.Id == bestellingId), Is.False);
        }

        [Test]
        public async Task DeleteBestelling_OnbestaandeId_GeeftNotFound()
        {
            // Act
            HttpResponseMessage deleteResponse = await _client.DeleteAsync("/bestellingen/99");

            // Assert
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestCase(4)]
        [TestCase(50)]
        [TestCase(100)]
        public async Task DeleteBestelling_VerschillendeOnbestaandeIds_GeeftNotFound(int id)
        {
            // Act
            HttpResponseMessage deleteResponse = await _client.DeleteAsync($"/bestellingen/{id}");

            // Assert
            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task FullCrudLifecycle_CreateReadUpdateDelete()
        {
            // Arrange - create a new bestelling
            var createBody = new Bestelling { Id = 0, Naam = "Festival Bestelling", Tafelnummer = 5, Gerechten = "Lasagne-Salade", Status = "InWachtrij" };
            
            var createContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(createBody), System.Text.Encoding.UTF8);
            createContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Step 1: POST - create the bestelling
            HttpResponseMessage postResponse = await _client.PostAsync("/bestellingen", createContent);
            var bestelling = await postResponse.Content.ReadFromJsonAsync<Bestelling>();
            int bestellingId = bestelling!.Id;

            Assert.That(postResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            // Step 2: GET - read the created bestelling
            HttpResponseMessage getResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");
            var retrievedBestelling = await getResponse.Content.ReadFromJsonAsync<Bestelling>();

            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(retrievedBestelling!.Id, Is.EqualTo(bestellingId));
            Assert.That(retrievedBestelling.Naam, Is.EqualTo("Festival Bestelling"));

            // Step 3: PUT - update the bestelling
            var updateBody = new Bestelling { Id = bestellingId, Naam = "Bijgewerkte Festival Bestelling", Tafelnummer = 10, Gerechten = "Risotto-Gegrilde Groenten", Status = "Gereed" };
            
            var updateContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(updateBody), System.Text.Encoding.UTF8);
            updateContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage putResponse = await _client.PutAsync($"/bestellingen/{bestellingId}", updateContent);

            Assert.That(putResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));

            // Step 4: GET - verify the update
            HttpResponseMessage getAfterPutResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");
            var updatedBestelling = await getAfterPutResponse.Content.ReadFromJsonAsync<Bestelling>();

            Assert.That(getAfterPutResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(updatedBestelling!.Id, Is.EqualTo(bestellingId));
            Assert.That(updatedBestelling.Naam, Is.EqualTo("Bijgewerkte Festival Bestelling"));
            Assert.That(updatedBestelling.Tafelnummer, Is.EqualTo(10));
            Assert.That(updatedBestelling.Gerechten, Is.EqualTo("Risotto-Gegrilde Groenten"));
            Assert.That(updatedBestelling.Status, Is.EqualTo("Gereed"));

            // Step 5: DELETE - delete the bestelling
            HttpResponseMessage deleteResponse = await _client.DeleteAsync($"/bestellingen/{bestellingId}");

            // Step 6: GET - verify deletion
            HttpResponseMessage getAfterDeleteResponse = await _client.GetAsync($"/bestellingen/{bestellingId}");

            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
            Assert.That(getAfterDeleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
