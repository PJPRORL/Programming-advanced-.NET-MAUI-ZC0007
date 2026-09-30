using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ProgrammingAdvancedOefeningen.Controllers.CompatibiliteitController;

namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompatibiliteitController : ControllerBase
    {
        [HttpGet]
        public string Welkomstbericht()
        {
            return "Compatibiliteitsbalie Bit & Byte. Geef twee onderdelen op om ze te vergelijken.";
        }

        [HttpGet("socket/{socketProcessor}/{socketMoederbord}")]
        public string VergelijkSockets(string socketProcessor, string socketMoederbord)
        {

            if (string.Equals(socketProcessor, socketMoederbord, StringComparison.OrdinalIgnoreCase))
            {
                return $"De processor socket {socketProcessor} past in moederbord socket {socketMoederbord}.";
            }

            return $"De processor past niet: {socketProcessor} tegenover {socketMoederbord}.";
        }

        //[HttpGet("voeding/{wattage}/{verbruik}")]
        //public string TotaalVerbruik(int wattage, int verbruik)
        //{

        //    if (wattage < verbruik)
        //    {
        //        return $"De voeding is te zwak voor deze build.";
        //    }
        //    else if (wattage - verbruik < Marge)
        //    {
        //        return $"De voeding volstaat, maar de marge is krap.";
        //    }

        //    return $"De voeding is ruim voldoende.";
        //}
        private const int Marge = 100;

        [HttpGet("voeding/{wat}/{gebruik}")]
        public string Verbruik(int wat, int gebruik) => (wat - gebruik) switch
        {
            < 0 => "De voeding is te zwak voor deze build.",
            < Marge => "De voeding volstaat, maar de marge is krap.",
            _ => "De voeding is ruim voldoende."
        };

        [HttpGet("/sloten/{aantalSloten}/{aantalModules}")]
        public string gebruikteSloten(int aantalSloten, int aantalModules)
        {
            if (aantalModules > aantalSloten)
            {
                return $"Er passen maar {aantalSloten} modules in dit moederbord.";
            }
            else if (aantalSloten == aantalModules)
            {
                return "Alle sloten worden gebruikt.";
            }

            int overgeblevenSloten = aantalSloten - aantalModules;

            return overgeblevenSloten == 1
            ? $"Er blijft nog {overgeblevenSloten} slot vrij."
            : $"Er blijven nog {overgeblevenSloten} sloten vrij.";
        }

        [HttpGet("build/{build}/{socketProcessor}/{wattage}/{aantalModules}")]
        public class Build
        {
            public string Type { get; set; } = "";
            public string Socket { get; set; } = "";
            public int GeheugenSloten { get; set; } = 0;
            public int Verbruik { get; set; } = 0;
        }

        private readonly List<Build> standaarBuild = new List<Build>
        {
           new Build { Type = "kantoor", Socket = "AM5", GeheugenSloten = 2, Verbruik = 180 },
           new Build { Type = "gaming", Socket = "AM5", GeheugenSloten = 4, Verbruik = 520 },
           new Build { Type = "montage", Socket = "LGA1700", GeheugenSloten = 4, Verbruik = 610 }
        };

        public Build BuildKiezer(string build, string socketProcessor, int wattage, int aantalModules)
        {
            
        }
    }
}
