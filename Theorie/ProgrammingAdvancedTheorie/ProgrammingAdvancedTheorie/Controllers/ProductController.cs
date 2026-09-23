using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingAdvancedTheorie.Models;

namespace ProgrammingAdvancedTheorie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private List<Laptop> laptops = new List<Laptop>
        {
            new Laptop { Id = 1, Merk = "Dell", Processor = "Intel i7", RamInGB = 16, Prijs = 1200.00, GPU = "NVIDIA GTX 1660" },
            new Laptop { Id = 2, Merk = "HP", Processor = "AMD Ryzen 5", RamInGB = 8, Prijs = 800.00, GPU = "AMD Radeon RX 560" },
            new Laptop { Id = 3, Merk = "Apple", Processor = "Apple M1", RamInGB = 16, Prijs = 1500.00, GPU = "Integrated" }
        };

        // Methods
        [HttpGet]
        public List<Laptop> GetAllLaptops()
        {
            return laptops;
        }
    }
}
