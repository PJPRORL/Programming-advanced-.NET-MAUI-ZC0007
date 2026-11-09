using System.ComponentModel.DataAnnotations;

namespace Examen_MVC.Models
{
    public class Brewer : IModel
    {
        public int Id { get; set; }

        [Required()]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Coffee> Coffees { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}