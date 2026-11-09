using System.ComponentModel.DataAnnotations;

namespace Examen_MVC.Models
{
    public class Coffee : IModel
    {
        private string image;

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }

        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }

        public string Image
        {
            get
            {
                if (image == null || string.IsNullOrWhiteSpace(image))
                {
                    return "unknown.jpg";
                }
                return image;
            }
            set => image = value;
        }

        [Required]
        public int BrewerId { get; set; }

        public Brewer Brewer { get; set; }

        public List<Cart> CoffeesInCart { get; set; } = new List<Cart>();
    }
}