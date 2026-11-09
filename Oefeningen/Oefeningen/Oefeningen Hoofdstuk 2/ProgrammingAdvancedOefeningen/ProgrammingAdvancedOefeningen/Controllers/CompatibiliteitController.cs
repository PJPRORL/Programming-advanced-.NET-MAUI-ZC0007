using Microsoft.AspNetCore.Mvc;

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

        public string VerkrijgenSockets(string socketProcessor, string socketMoederbord)
        {
            return VergelijkSockets(socketProcessor, socketMoederbord);
        }
        private static string VergelijkSockets(string socketProcessor, string socketMoederbord)
        {

            if (string.Equals(socketProcessor, socketMoederbord, StringComparison.OrdinalIgnoreCase))
            {
                return "De processor past op dit moederbord.";
            }

            return $"De processor past niet: {socketProcessor} tegenover {socketMoederbord}.";
        }

        [HttpGet("voeding/{wattage}/{verbruik}")]

        public string VoedingAdvies(int wattage, int verbruik)
        {
            return MargeVoeding(wattage, verbruik);
        }

        private const int Marge = 100;

        private static string MargeVoeding(int berekenWattage, int berekenGebruik) => (berekenWattage - berekenGebruik) switch
        {
            < 0 => "De voeding is te zwak voor deze build.",
            < Marge => "De voeding volstaat, maar de marge is krap.",
            _ => "De voeding is ruim voldoende."
        };

        [HttpGet("sloten/{aantalSloten}/{aantalModules}")]

        public string OpvragenGeheugen(int aantalSloten, int aantalModules)
        {
            return GebruikteSloten(aantalSloten, aantalModules);
        }

        private static string GebruikteSloten(int sloten, int modules)
        {
            if (modules > sloten)
            {
                return $"Er passen maar {sloten} modules in dit moederbord.";
            }
            else if (sloten == modules)
            {
                return "Alle sloten worden gebruikt.";
            }

            int overgeblevenSloten = sloten - modules;

            return overgeblevenSloten == 1
            ? $"Er blijft nog {overgeblevenSloten} slot vrij."
            : $"Er blijven nog {overgeblevenSloten} sloten vrij.";
        }

        [HttpGet("build/{build}/{socketProcessor}/{wattage}/{aantalModules}")]

        public string InformatieBuild(string build, string socketProcessor, int wattage, int aantalModules)
        {
            string socketMoederbord;
            int verbruik;
            int sloten;

            switch (build)
            {
                case "kantoor":
                    socketMoederbord = "AM5";
                    verbruik = 180;
                    sloten = 2;
                    break;
                case "gaming":
                    socketMoederbord = "AM5";
                    verbruik = 520;
                    sloten = 4;
                    break;
                case "montage":
                    socketMoederbord = "LGA1700";
                    verbruik = 610;
                    sloten = 4;
                    break;
                default:
                    return "We kennen deze build niet.";
            }

            string socket = VerkrijgenSockets(socketProcessor, socketMoederbord);
            string voeding = VoedingAdvies(wattage, verbruik);
            string slot = OpvragenGeheugen(sloten, aantalModules);

            return $"{socket} {voeding} {slot}";
        }

    }
}
