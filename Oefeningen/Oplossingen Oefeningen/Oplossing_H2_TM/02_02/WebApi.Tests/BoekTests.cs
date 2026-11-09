using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Tests
{
    public class BoekEndpointsTests
    {
        private WebApplicationFactory<BoekController> _factory = null!;
        private HttpClient _client = null!;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<BoekController>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetBoeken_GeeftAlleBoekenMetStatusOk()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/boeken");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetBoeken_GeeftDrieSeedBoekenTerug()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/boeken");
            var boeken = await response.Content.ReadFromJsonAsync<List<Boek>>();

            // Assert
            Assert.That(boeken, Is.Not.Null);
            Assert.That(boeken!.Count, Is.EqualTo(3));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetBoeken_InhoudBevatSeedData(int id)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/boeken");
            var boeken = await response.Content.ReadFromJsonAsync<List<Boek>>();

            // Assert
            Assert.That(boeken, Is.Not.Null);
            var boek = boeken!.FirstOrDefault(b => b.Id == id);
            Assert.That(boek, Is.Not.Null);

            switch (id)
            {
                case 1:
                    Assert.That(boek.Titel, Is.EqualTo("De Ontdekking van de Hemel"));
                    Assert.That(boek.Auteur, Is.EqualTo("Harry Mulisch"));
                    Assert.That(boek.Uitgeverij, Is.EqualTo("De Arbeiderspers"));
                    Assert.That(boek.Jaartal, Is.EqualTo(1992));
                    break;
                case 2:
                    Assert.That(boek.Titel, Is.EqualTo("Het Dagboek van Anne Frank"));
                    Assert.That(boek.Auteur, Is.EqualTo("Anne Frank"));
                    Assert.That(boek.Uitgeverij, Is.EqualTo("Contact"));
                    Assert.That(boek.Jaartal, Is.EqualTo(1947));
                    break;
                case 3:
                    Assert.That(boek.Titel, Is.EqualTo("De Avonturen van Pi"));
                    Assert.That(boek.Auteur, Is.EqualTo("Yann Martel"));
                    Assert.That(boek.Uitgeverij, Is.EqualTo("De Bezige Bij"));
                    Assert.That(boek.Jaartal, Is.EqualTo(2001));
                    break;
            }
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetBoek_MetId_GeeftCorrectBoekMetStatusOk(int id)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/boeken/{id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [TestCase(1, "De Ontdekking van de Hemel", "Harry Mulisch", "De Arbeiderspers", 1992)]
        [TestCase(2, "Het Dagboek van Anne Frank", "Anne Frank", "Contact", 1947)]
        [TestCase(3, "De Avonturen van Pi", "Yann Martel", "De Bezige Bij", 2001)]
        public async Task GetBoek_MetId_GeeftCorrecteInhoud(int id, string titel, string auteur, string uitgeverij, int jaartal)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/boeken/{id}");
            var boek = await response.Content.ReadFromJsonAsync<Boek>();

            // Assert
            Assert.That(boek, Is.Not.Null);
            Assert.That(boek!.Id, Is.EqualTo(id));
            Assert.That(boek.Titel, Is.EqualTo(titel));
            Assert.That(boek.Auteur, Is.EqualTo(auteur));
            Assert.That(boek.Uitgeverij, Is.EqualTo(uitgeverij));
            Assert.That(boek.Jaartal, Is.EqualTo(jaartal));
        }

        [Test]
        public async Task GetBoek_OnbestaandeId_GeeftNotFound()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/boeken/99");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [TestCase(4)]
        [TestCase(50)]
        [TestCase(100)]
        public async Task GetBoek_VerschillendeOnbestaandeIds_GeeftNotFound(int id)
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync($"/boeken/{id}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostBoek_GeeftCreatedMetStatus201()
        {
            // Arrange
            var body = new Boek { Id = 0, Titel = "De Kringloop van het Leven", Auteur = "Hella Haasse", Uitgeverij = "Contact", Jaartal = 1960 };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/boeken", content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task PostBoek_GeeftTerugHetGemaakteBoek()
        {
            // Arrange
            var body = new Boek { Id = 0, Titel = "De Kringloop van het Leven", Auteur = "Hella Haasse", Uitgeverij = "Contact", Jaartal = 1960 };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/boeken", content);
            var boek = await response.Content.ReadFromJsonAsync<Boek>();

            // Assert
            Assert.That(boek, Is.Not.Null);
            Assert.That(boek!.Titel, Is.EqualTo("De Kringloop van het Leven"));
            Assert.That(boek.Auteur, Is.EqualTo("Hella Haasse"));
            Assert.That(boek.Uitgeverij, Is.EqualTo("Contact"));
            Assert.That(boek.Jaartal, Is.EqualTo(1960));
        }

        [Test]
        public async Task PostBoek_BerekentIdAlsMaxPlusEen()
        {
            // Arrange - get current max ID first
            HttpResponseMessage getResponse = await _client.GetAsync("/boeken");
            var boeken = await getResponse.Content.ReadFromJsonAsync<List<Boek>>();

            int maxId = boeken!.Max(b => b.Id);

            var body = new Boek { Id = 0, Titel = "De Kringloop van het Leven", Auteur = "Hella Haasse", Uitgeverij = "Contact", Jaartal = 1960 };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/boeken", content);
            var boek = await response.Content.ReadFromJsonAsync<Boek>();

            // Assert
            Assert.That(boek!.Id, Is.EqualTo(maxId + 1));
        }

        [Test]
        public async Task PostBoek_IgnoreertIdUitRequestBody()
        {
            // Arrange - send a book with id=999 which should be ignored
            var body = new Boek { Id = 999, Titel = "De Kringloop van het Leven", Auteur = "Hella Haasse", Uitgeverij = "Contact", Jaartal = 1960 };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync("/boeken", content);
            var boek = await response.Content.ReadFromJsonAsync<Boek>();

            // Assert - the returned ID should not be 999
            Assert.That(boek!.Id, Is.Not.EqualTo(999));
        }

        [Test]
        public async Task PostBoek_NaCreate_GeeftBoekTerugViaGet()
        {
            // Arrange
            var body = new Boek { Id = 0, Titel = "De Kringloop van het Leven", Auteur = "Hella Haasse", Uitgeverij = "Contact", Jaartal = 1960 };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act - create the book
            HttpResponseMessage postResponse = await _client.PostAsync("/boeken", content);
            var boek = await postResponse.Content.ReadFromJsonAsync<Boek>();

            // Act - get the book by its returned ID
            int createdId = boek!.Id;
            HttpResponseMessage getResponse = await _client.GetAsync($"/boeken/{createdId}");
            var retrievedBoek = await getResponse.Content.ReadFromJsonAsync<Boek>();

            // Assert
            Assert.That(getResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(retrievedBoek!.Id, Is.EqualTo(createdId));
            Assert.That(retrievedBoek.Titel, Is.EqualTo("De Kringloop van het Leven"));
            Assert.That(retrievedBoek.Auteur, Is.EqualTo("Hella Haasse"));
            Assert.That(retrievedBoek.Uitgeverij, Is.EqualTo("Contact"));
            Assert.That(retrievedBoek.Jaartal, Is.EqualTo(1960));
        }

        [Test]
        public async Task PostBoek_NaCreate_VerschenenInCollectie()
        {
            // Arrange
            var body = new Boek { Id = 0, Titel = "De Kringloop van het Leven", Auteur = "Hella Haasse", Uitgeverij = "Contact", Jaartal = 1960 };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(body), System.Text.Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Act - get count before
            HttpResponseMessage getBeforeResponse = await _client.GetAsync("/boeken");
            var boekenBefore = await getBeforeResponse.Content.ReadFromJsonAsync<List<Boek>>();

            // Act - create the book
            HttpResponseMessage postResponse = await _client.PostAsync("/boeken", content);

            // Act - get count after
            HttpResponseMessage getAfterResponse = await _client.GetAsync("/boeken");
            var boekenAfter = await getAfterResponse.Content.ReadFromJsonAsync<List<Boek>>();

            // Assert
            Assert.That(boekenAfter!.Count, Is.EqualTo(boekenBefore!.Count + 1));
        }

        [Test]
        public async Task PostBoek_MetVerschillendeData_GeeftCorrecteId()
        {
            // Arrange - first create one book to verify ID calculation works for subsequent books too
            var body1 = new Boek { Id = 0, Titel = "Eerste Boek", Auteur = "Auteur A", Uitgeverij = "Uitgeverij A", Jaartal = 2000 };
            var content1 = new StringContent(System.Text.Json.JsonSerializer.Serialize(body1), System.Text.Encoding.UTF8);
            content1.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage post1Response = await _client.PostAsync("/boeken", content1);
            var boek1 = await post1Response.Content.ReadFromJsonAsync<Boek>();

            // Arrange - get current max ID after first creation
            int maxIdAfterFirst = boek1!.Id;

            // Act - create second book
            var body2 = new Boek { Id = 0, Titel = "Tweede Boek", Auteur = "Auteur B", Uitgeverij = "Uitgeverij B", Jaartal = 2005 };
            var content2 = new StringContent(System.Text.Json.JsonSerializer.Serialize(body2), System.Text.Encoding.UTF8);
            content2.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage post2Response = await _client.PostAsync("/boeken", content2);
            var boek2 = await post2Response.Content.ReadFromJsonAsync<Boek>();

            // Assert
            Assert.That(boek2!.Id, Is.EqualTo(maxIdAfterFirst + 1));
        }
    }
}
