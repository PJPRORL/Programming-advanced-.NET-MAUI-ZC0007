using Microsoft.AspNetCore.Mvc.Rendering;

namespace Examen_MVC.Models
{
    public class AddCoffeeVM
    {
        // Todo: Voeg Data annotations toe
        public string Description { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int BrewerId { get; set; }

        public List<SelectListItem>? Brewer { get; set; }
    }
}