namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoekController : ControllerBase
    {
        List<Boek> Boeken = new List<Boek>
        {
            new Boek { Id = 1, Titel = "De Ontdekking van de Hemel", Auteur = "Harry Mulisch", Uitgeverij = "De Arbeiderspers", Jaartal = 1992 },
            new Boek { Id = 2, Titel = "Het Dagboek van Anne Frank", Auteur = "Anne Frank", Uitgeverij = "Contact", Jaartal = 1947 },
            new Boek { Id = 3, Titel = "De Avonturen van Pi", Auteur = "Yann Martel", Uitgeverij = "De Bezige Bij", Jaartal = 2001 }
        };

        [HttpGet("boeken")]
        public ActionResult<List<Boek>> GetBoeken()
        {
            return Ok(Boeken);
        }

        [HttpGet("boeken/{id}")]
        public ActionResult<List<Boek>> GetBoekById(int id)
        {
            Boek? boek = Boeken.FirstOrDefault(boek => boek.Id == id);

            if (boek == null)
            {
                return NotFound($"We vonden geen artikel met Id {id}.");
            }

            return Ok(boek);
        }
    }
}
