using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingAdvancedTheorie.Models;

namespace ProgrammingAdvancedTheorie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Onze tijdelijke "database" in het geheugen
        private List<Laptop> laptops = new List<Laptop>
    {
        new Laptop { Id = 1, Merk = "Dell", Processor = "Intel i7", RamInGB = 16, Prijs = 1200.00, GPU = "Iris Xe" },
        new Laptop { Id = 2, Merk = "Apple", Processor = "M3 Pro", RamInGB = 18, Prijs = 2500.00, GPU = "Apple GPU" },
        new Laptop { Id = 3, Merk = "Lenovo", Processor = "AMD Ryzen 7", RamInGB = 32, Prijs = 1400.00, GPU = "RTX 4060" }
    };

        // 1. GET: Alle laptops ophalen
        // Route: GET api/laptops
        [HttpGet]
        public ActionResult<List<Laptop>> GetLaptops()
        {
            // We sturen een 200 OK status terug samen met de lijst
            return Ok(laptops);
        }

        // 2. GET: Eén specifieke laptop op basis van Id
        // Route: GET api/laptops/1
        [HttpGet("{id}")]
        public ActionResult<Laptop> GetLaptopById(int id)
        {
            // Zoek de laptop in de lijst die de gevraagde Id heeft
            var laptop = laptops.FirstOrDefault(x => x.Id == id);

            // Als de laptop niet bestaat, stuur dan een 404 terug
            if (laptop == null)
            {
                return NotFound($"Helaas, we konden geen laptop vinden met Id {id}.");
            }

            return Ok(laptop);
        }
    }
}
