namespace ProgrammingAdvancedOefeningen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestellijnController : ControllerBase
    {
        private static List<Bestellijn> bestellijnen = new List<Bestellijn>
        {
            new Bestellijn {Id = 1, Aantal = 3, IsActief = true, Omschrijving = "Noctua NF-A12x25 ventilator", StukPrijs = 35},
            new Bestellijn {Id = 2, Aantal = 1, IsActief = true, Omschrijving = "Corsair Vengeance 32GB DDR5", StukPrijs = 110},
            new Bestellijn {Id = 3, Aantal = 2, IsActief = false, Omschrijving = "Samsung 990 Pro 2TB SSD", StukPrijs = 170},
            new Bestellijn {Id = 4, Aantal = 1, IsActief = true, Omschrijving = "be quiet! Pure Power 12 M 750W", StukPrijs = 95},
            new Bestellijn {Id = 5, Aantal = 4, IsActief = false, Omschrijving = "Arctic MX-6 koelpasta", StukPrijs = 9}
        };

        [HttpGet()]
        public ActionResult<List<Bestellijn>> GetActieveBestellingen()
        {
            List<Bestellijn> actieveBestellijnen = new List<Bestellijn>();

            actieveBestellijnen = bestellijnen.Where(bestellijn => bestellijn.IsActief).ToList();

            //foreach (var bestelling in bestellijnen)
            //{
            //    if (bestelling.IsActief)
            //    {
            //        actieveBestellijnen.Add(bestelling);
            //    }
            //}

            return Ok(actieveBestellijnen);
        }

        [HttpGet("alles")]
        public ActionResult<List<Bestellijn>> GetAlleBestellingen()
        {
            return Ok(bestellijnen);
        }

        [HttpGet("{id}")]
        public ActionResult<Bestellijn> GetBestellingById(int id)
        {
            Bestellijn? bestellijn = bestellijnen.FirstOrDefault(lijn => lijn.Id == id);

            if (bestellijn == null || bestellijn.IsActief == false)
            {
                return NotFound($"We vonden geen actieve bestellijn met Id {id}.");
            }

            return Ok(bestellijn);
        }

        [HttpPost()]
        public ActionResult<Bestellijn> ToevoegenBestellijn(Bestellijn nieuweBestellijn)
        {
            if (string.IsNullOrWhiteSpace(nieuweBestellijn.Omschrijving))
            {
                return NotFound("Een bestellijn moet een omschrijving hebben.");
            }

            if (nieuweBestellijn.Aantal < 1)
            {
                return NotFound("Het aantal moet minstens 1 zijn.");
            }

            if (nieuweBestellijn.StukPrijs <= 0)
            {
                return NotFound("De stukprijs moet groter zijn dan 0.");
            }
            else
            {
                int nieuweId = bestellijnen.Max(lijn => lijn.Id) + 1;
                nieuweBestellijn.Id = nieuweId;

                bestellijnen.Add(nieuweBestellijn);
            }

            return CreatedAtAction(nameof(GetBestellingById), new { id = nieuweBestellijn.Id }, nieuweBestellijn);
        }
        [HttpPut("{id}")]
        public ActionResult<List<Bestellijn>> PasBestellijnAan(int id, Bestellijn bijgewerkteBestellijn)
        {
            Bestellijn? bestaandeBestellijn = bestellijnen.FirstOrDefault(lijn => lijn.Id == id);

            if (bestaandeBestellijn == null || bestaandeBestellijn.IsActief == false)
            {
                return NotFound("Werkt enkel op actieve lijnen");
            }

            if (string.IsNullOrWhiteSpace(bijgewerkteBestellijn.Omschrijving))
            {
                return BadRequest("Een bestellijn moet een omschrijving hebben.");
            }

            if (bijgewerkteBestellijn.Aantal < 1)
            {
                return BadRequest("Het aantal moet minstens 1 zijn.");
            }

            if (bijgewerkteBestellijn.StukPrijs <= 0)
            {
                return BadRequest("De stukprijs moet groter zijn dan 0.");
            }

            bestaandeBestellijn.Omschrijving = bijgewerkteBestellijn.Omschrijving;
            bestaandeBestellijn.Aantal = bijgewerkteBestellijn.Aantal;
            bestaandeBestellijn.StukPrijs = bijgewerkteBestellijn.StukPrijs;

            return NoContent();
        }


    }
}
