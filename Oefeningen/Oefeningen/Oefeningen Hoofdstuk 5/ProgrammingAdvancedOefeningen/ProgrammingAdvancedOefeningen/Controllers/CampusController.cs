namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampusController : ControllerBase
    {
        [HttpGet]
        public string WelkomstBericht()
        {
            return "Welkom bij Northwind College!";
        }

        [HttpGet("welkom/{naam}")]
        public string PersoonlijkBericht(string naam)
        {
            return $"Welkom Welkom bij Northwind College, {naam}";
        }

        [HttpGet("gebouw/{gebouw}")]
        //public string GeefGebouw(string gebouw)
        //{
        //    //string keuze = gebouw;

        //    switch (gebouw.ToLower())
        //    {
        //        case "bibliotheek":
        //            return "In de bibliotheek kan je studeren en boeken ontlenen.";
        //        case "sport":
        //            return "In het sportgebouw vind je de fitnessruimte en indoor sportzalen.";
        //        case "technologie":
        //            return "In het technologiegebouw vind je de computerlokalen.";
        //        default:
        //            return "Sorry, we hebben geen informatie over dit gebouw.";
        //    }

        //    return gebouw;
        //}

        public string GeefGebouw(string gebouw) => gebouw.ToLower() switch
        {
            "bibliotheek" => "In de bibliotheek kan je studeren en boeken ontlenen.",
            "sport" => "In het sportgebouw vind je de fitnessruimte en indoor sportzalen.",
            "technologie" => "In het technologiegebouw vind je de computerlokalen.",
            _ => "Sorry, we hebben geen informatie over dit gebouw."
        };

        [HttpGet("les/{minuten}")]
        public string GeefLesInfo(int minuten) => minuten switch
        {
            < 10 => "Ga nu naar je leslokaal.",
            >= 10 and <= 30 => "Je hebt nog even tijd voor je les begint.",
            > 30 => "Je hebt nog ruim voldoende tijd voor je les.",
        };
    }
}
