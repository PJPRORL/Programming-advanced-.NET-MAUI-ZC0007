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
    public class BoekEndpointsTests
    {
        private WebApplicationFactory<BoekController> _factory = null!;
        private HttpClient _client = null!;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<BoekController>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(IBoekRepository));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddSingleton<IBoekRepository, InMemoryBoekRepository>();
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
    }
}
