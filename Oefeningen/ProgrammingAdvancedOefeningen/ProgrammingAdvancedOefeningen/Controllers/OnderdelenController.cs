using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnderdelenController : ControllerBase
    {
        [HttpGet]
        public string Welkomstbericht()
        {
            return "Welkom bij de onderdelenbalie van Bit & Byte.";
        }

        private static readonly Dictionary<string, string> onderdelen = new(StringComparer.OrdinalIgnoreCase)
        {
            ["moederbord"] = "Het moederbord verbindt alle onderdelen met elkaar.",
            ["processor"] = "De processor voert alle berekeningen uit.",
            ["videokaart"] = "De videokaart tekent het beeld voor je scherm.",
        };
        /* Word gebruikt om de informatie op te slagen. Je geeft 2 waarden mee <string, string>,
         * deze worden dan later gekoppeld aan de waarden: [waarden1], "Waarden2" in de Dictionary*/

        [HttpGet("soort/{soort}")]
        public string OnderdeelSoort(string soort) =>
            onderdelen.TryGetValue(soort, out string? tekst)
            /*onderdelen.TryGetValue hangt vast aan
             *Dictionary onderdelen die je erboven hebt aangemaakt*/
            ? tekst
            : "Sorry, we hebben geen informatie over dit onderdeel.";

        [HttpGet("voorraad/{aantal}")]

        public string OpVoorraad(int aantal) => aantal switch
        {
            0 => "Niet op voorraad, bestel bij de leverancier.",
            1 or <= 4 => "Beperkt op voorraad, hou dit in het oog.",
            > 4 => "Ruim op voorraad."
        };

        [HttpGet("code/{artikelcode}")]
        public string ArtikelcodeOpzoeken(string code)
        {
            return $"Artikelcode {code} telt {code.Length} tekens.";
        }

        public class Onderdeel
        {
            public string Type { get; set; } = "";
            public string Artikelcode { get; set; } = "";
            public int Voorraad { get; set; } = 0;
            public string Description { get; set; } = "";
        };

        public List<Onderdeel> Onderdelen = new() {
            new Onderdeel { Type = "moederbord", Artikelcode = "MB-B650-01", Voorraad = 12, Description = "Het moederbord verbindt alle onderdelen met elkaar." },
            new Onderdeel { Type = "processor", Artikelcode = "CPU-7600X-01", Voorraad = 3, Description = "De processor voert alle berekeningen uit." },
            new Onderdeel { Type = "videokaart", Artikelcode = "GPU-4070-01", Voorraad = 0, Description = "De videokaart tekent het beeld voor je scherm." }
        };

        [HttpGet("fiche/{soort}")]

        public Onderdeel InformatiePerOnderdeel(List<Onderdeel> type)
        {
            return "";
        }
    }
}
