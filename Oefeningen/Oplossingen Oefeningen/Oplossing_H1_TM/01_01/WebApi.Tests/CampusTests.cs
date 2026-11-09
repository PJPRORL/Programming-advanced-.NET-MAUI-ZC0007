using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using WebApi.Controllers;

namespace WebApi.Tests
{
    public class CampusEndpointsTests
    {
        private WebApplicationFactory<CampusController> _factory = null!;
        private HttpClient _client = null!;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<CampusController>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetCampus_GeeftAlgemeenWelkomstbericht()
        {
            // Act
            HttpResponseMessage response = await _client.GetAsync("/campus");
            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(inhoud, Is.EqualTo("Welkom bij Northwind College!"));
        }

        [Test]
        public async Task GetWelkom_MetNaam_GeeftPersoonlijkWelkomstbericht()
        {
            // Arrange
            string naam = "Amina";

            // Act
            HttpResponseMessage response =
                await _client.GetAsync($"/campus/welkom/{naam}");

            string inhoud =
                await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
            Assert.That(
                inhoud,
                Is.EqualTo("Welkom bij Northwind College, Amina!")
            );
        }

        [TestCase("Noah")]
        [TestCase("Fatima")]
        [TestCase("Yuki")]
        public async Task GetWelkom_MetVerschillendeNamen_GebruiktNaamUitRoute(
    string naam)
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync($"/campus/welkom/{naam}");

            string inhoud =
                await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo($"Welkom bij Northwind College, {naam}!")
            );
        }

        [Test]
        public async Task GetGebouw_Bibliotheek_GeeftCorrecteInformatie()
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync("/campus/gebouw/bibliotheek");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo(
                    "In de bibliotheek kan je studeren en boeken ontlenen."
                )
            );
        }

        [Test]
        public async Task GetGebouw_Sport_GeeftCorrecteInformatie()
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync("/campus/gebouw/sport");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo(
                    "In het sportgebouw vind je de fitnessruimte en indoor sportzalen."
                )
            );
        }

        [Test]
        public async Task GetGebouw_Technologie_GeeftCorrecteInformatie()
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync("/campus/gebouw/technologie");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo(
                    "In het technologiegebouw vind je de computerlokalen."
                )
            );
        }

        [TestCase("cafetaria")]
        [TestCase("parking")]
        [TestCase("restaurant")]
        [TestCase("wetenschap")]
        public async Task GetGebouw_OnbekendGebouw_GeeftStandaardbericht(
            string gebouw)
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync($"/campus/gebouw/{gebouw}");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo(
                    "Sorry, we hebben geen informatie over dit gebouw."
                )
            );
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(9)]
        public async Task GetLes_MinderDanTienMinuten_ZegtOmNaarLesTeGaan(
        int minuten)
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync($"/campus/les/{minuten}");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(inhoud, Is.EqualTo("Ga nu naar je leslokaal."));
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(20)]
        [TestCase(29)]
        [TestCase(30)]
        public async Task GetLes_TussenTienEnDertigMinuten_GeeftEvenTijd(
            int minuten)
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync($"/campus/les/{minuten}");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo("Je hebt nog even tijd voor je les begint.")
            );
        }

        [TestCase(31)]
        [TestCase(45)]
        [TestCase(60)]
        [TestCase(120)]
        public async Task GetLes_MeerDanDertigMinuten_GeeftRuimVoldoendeTijd(
            int minuten)
        {
            // Act
            HttpResponseMessage response =
                await _client.GetAsync($"/campus/les/{minuten}");

            string inhoud = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(
                inhoud,
                Is.EqualTo("Je hebt nog ruim voldoende tijd voor je les.")
            );
        }
    }
}
