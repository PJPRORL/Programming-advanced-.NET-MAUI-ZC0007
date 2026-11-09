using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security;

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

        private const string OnbekendeSoort = "Sorry, dit soort onderdeel verkopen wij niet.";

        [HttpGet("soort/{soort}")]
        public string OnderdeelSoortUitleg(string soort)
        {
            return UitlegVoorSoort(soort);
        }

        private string UitlegVoorSoort(string soort)
        {
            switch (soort)
            {
                case "moederbord":
                    return "Het moederbord verbindt alle onderdelen met elkaar.";
                case "processor":
                    return "De processor voert alle berekeningen uit.";
                case "videokaart":
                    return "De videokaart tekent het beeld voor je scherm.";
                default:
                    return OnbekendeSoort;
            }
        }

        [HttpGet("voorraad/{aantal}")]

        public string VoorraadStatus(int aantal)
        {
            return OpVoorraad(aantal);
        }

        private string OpVoorraad(int aantal) => aantal switch
        {
            0 => "Niet op voorraad, bestel bij de leverancier.",
            1 or <= 4 => "Beperkt op voorraad, hou dit in het oog.",
            > 4 => "Ruim op voorraad."
        };

        [HttpGet("code/{artikelcode}")]
        public string ArtikelcodeKrijgen(string code)
        {
            return CodeberichtVoor(code);
        }

        private string CodeberichtVoor(string artikelcode)
        {
            return $"Artikelcode {artikelcode.ToUpper()} telt {artikelcode.Length} tekens.";
        }

        [HttpGet("fiche/{soort}")]

        public string InformatiePerOnderdeel(string soort)
        {
            string artikelcode = "";
            int voorraad = 0;

            switch (soort)
            {
                case "moederbord":
                    artikelcode = "MB-B650-01";
                    voorraad = 12;
                    break;
                case "processor":
                    artikelcode = "CPU-7600X-01";
                    voorraad = 3;
                    break;
                case "videokaart":
                    artikelcode = "GPU-4070-01";
                    voorraad = 0;
                    break;
                default:
                    return OnbekendeSoort;
            }

            return $"{OnderdeelSoortUitleg(soort)} {VoorraadStatus(voorraad)} {ArtikelcodeKrijgen(artikelcode)}";
        }
    }
}
